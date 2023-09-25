using Film.Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Film.Domain.Contract.Base.Models;
using System.Linq.Expressions;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Film.Infrastructure.Persistance.Context;
using Film.Domain.Contract.Base.Repository;

namespace Film.Infrastructure.Persistance.Repositories.Base
{
    public class BaseRepository<TEntity>: IBaseRepository where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _entity;
        protected readonly FilmDbContext _context;

        public IUnitOfWork UnitOfWork{ get; private set; } 
        public BaseRepository(FilmDbContext context,IUnitOfWork unitOfWork)
        {
            _entity = context.Set<TEntity>();
            _context = context;
            UnitOfWork = unitOfWork;
        }
        protected Expression<Func<TEntity, bool>> Predicate<TFilter>(TFilter filter) where TFilter : BaseModel
        {
            var param = Expression.Parameter(typeof(TEntity), "u");
            Expression body = null;

            var entityPropertyNames = (typeof(TEntity).GetProperties()).Select(p => p.Name);

            foreach (var propertyInfo in typeof(TFilter).GetProperties())
            {
                var modelValue = propertyInfo.GetValue(filter);
                if (ShouldContinue(propertyInfo, modelValue) || !entityPropertyNames.Contains(propertyInfo.Name))
                    continue;

                var equalExpression = BuildExpression(param, propertyInfo.PropertyType, modelValue, propertyInfo.Name);

                body = body != null ?
                    Expression.AndAlso(body, equalExpression) :
                    equalExpression;
            }

            if (body is null)
            {
                return _ => true;
            }

            return Expression.Lambda<Func<TEntity, bool>>(body, param);
        }

        private static Expression BuildExpression(ParameterExpression param, Type propertyType, object modelValue, string propertyName)
        {
            var propertyValue = Expression.Constant(modelValue, propertyType);

            if (propertyType == typeof(string))
            {
                var exprop = Expression.Property(param, propertyName);
                var constant = Expression.Constant(modelValue);

                return Expression.Call(exprop, "Contains", Type.EmptyTypes, constant);
            }
            if (propertyType == typeof(bool?))
            {
                var notNullableBool = Expression.Convert(propertyValue, typeof(bool));
                return Expression.Equal(
                    Expression.Property(param, propertyName),
                    notNullableBool);
            }

            return Expression.Equal(
            Expression.Property(param, propertyName),
            propertyValue);
        }
        private static bool ShouldContinue(PropertyInfo propertyInfo, object modelValue)
        {
            if (modelValue is null || propertyInfo is null)
            {
                return true;
            }

            var propertyName = propertyInfo.Name;
            var propertyType = propertyInfo.PropertyType;

            var defaultvalue = propertyType.GetDefaultValue();

            if (defaultvalue?.Equals(modelValue) == true)
            {
                return true;
            }
            if (string.Equals(propertyName, nameof(PageRequestModel.PageIndex), StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (string.Equals(propertyName, nameof(PageRequestModel.PageSize), StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (string.Equals(propertyName, nameof(PageRequestModel.OrderByDesces), StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (string.Equals(propertyName, nameof(PageRequestModel.OrderByAsces), StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
        private async Task<List<int>> GetUpdCodes(ICollection<BulkRequestModel> bulkRequests)
        {
            return await _entity.Select(s => new
            {
                s.Hash,
                s.Code
            })
                                .Join(bulkRequests,
                                      db => db.Code,
                                      model => model.Code,
                                      (db, model) => new
                                      {
                                          Code = db.Code,
                                          HashDB = db.Hash,
                                          HashModel = model.Hash
                                      })
                                .Where(w => w.HashDB != w.HashModel)
                                .Select(s => s.Code)
                                .ToListAsync();
        }
        private async Task<List<int>> GetInsertCodes(ICollection<BulkRequestModel> bulkRequests)
        {
            return await _entity.Select(s => s.Code).Except(bulkRequests.Select(s => s.Code)).ToListAsync();
        }
        public async Task<BulkResponseModel> GetIdsFromHash(ICollection<BulkRequestModel> bulkRequests)
        {
            var result = new BulkResponseModel();
            await Task.WhenAll(
                Task.Run(async () =>
                {
                    result.UpdateCodes = await GetUpdCodes(bulkRequests);
                }),
                Task.Run(async () =>
                {
                    result.InsertCodes = await GetInsertCodes(bulkRequests);
                })
                );
            return result;
        }

        public async Task<int> GetMaxId()
        {
            return (await _entity.MaxAsync(m => m.Code)) + 1;
        }
    }
}