using Film.Domain.Contract.Base.Repository;
using Film.Domain.Contract.Category;
using Film.Domain.Contract.Category.Models;
using Film.Infrastructure.Persistance.Base;
using Film.Infrastructure.Persistance.Context;
using Film.Infrastructure.Persistance.Extentions;
using Film.Infrastructure.Persistance.Repositories.Base;
using Film.Infrastructure.Persistance.Repositories.Film;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Category.Infrastructure.Persistance.Repositories.Category
{
    public class CategoryRepository : CreateRepository<Film.Domain.Enities.Category>,ICategoryRepository
    {
        private readonly CategoryMapper _mapper;
        public CategoryRepository(FilmDbContext context,IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _mapper = new CategoryMapper();
        }

        public async Task Delete(Film.Domain.Enities.Category category)
        {
           await Task.FromResult(_entity.Remove(category));
        }

        public async Task<CategorDetailResponseModel> GetDetailAsync(int code)
        {
            return await _entity.Select(s=>_mapper.CategoryDetailResponseModel(s))
                                .FirstOrDefaultAsync(e => e.Code == code);

        }

        public async Task<Film.Domain.Contract.Base.Models.PageResponseModel<CategoriesListResponseModel>> GetListAsync(CategoriesListFilterRequestModel filter)
        {
            var predicate = Predicate(filter);

            var query = _entity.Where(predicate).AsQueryable();

            return await query.SortDynamic(filter.OrderByAsces, filter.OrderByDesces)
            .Select(s => _mapper.CategorysListResponseModel(s))
                         .PagingAsync(filter.PageSize, filter.PageIndex);

        }

        public int Update(Film.Domain.Enities.Category entity)
        {
            var updQuery = $"Update Category set " +
                $"{nameof(Film.Domain.Enities.Category.Name)}='{entity.Name}'," +
                $"{nameof(Film.Domain.Enities.Category.Description)}='{entity.Description}'," +
                $"{nameof(Film.Domain.Enities.Category.IsEnabled)}={entity.IsEnabled}," +
                $"{nameof(Film.Domain.Enities.Category.Priority)}={entity.Priority}," +
                $"{nameof(Film.Domain.Enities.Category.LastUpdate)}=GetDate()," +
                $"{nameof(Film.Domain.Enities.Category.Hash)}={entity.Hash}" +
                $"Where {nameof(Film.Domain.Enities.Category.Code)}={entity.Code} and {nameof(Film.Domain.Enities.Category.RowVersion)}={entity.RowVersion}";
            return _context.Database.ExecuteSqlRaw(updQuery);
        }

        public async Task<int> UpdateAsync(Film.Domain.Enities.Category entity)
        {
            var category = await _entity.FirstOrDefaultAsync(e => e.Code == entity.Code);
            if (category is null)
            {
                throw new PersisException("category not found", Film.Application.Base.BusinessExceptionType.NotFound);
            }
            if(category.RowVersion!=entity.RowVersion)
            {
                throw new PersisException("vaersion of is changed in database please refresh this data", Film.Application.Base.BusinessExceptionType.NotFound);
            }
            var entry = _context.Entry(entity);
            if (entity.IsEnabled != category.IsEnabled)
            {
                entry.Property(e => e.IsEnabled).IsModified = true;
            }
            if (entity.Priority != category.Priority)
            {
                entry.Property(e => e.Priority).IsModified = true;
            }
            if (string.Equals(entity.Description, category.Description, StringComparison.CurrentCultureIgnoreCase))
            {
                entry.Property(e => e.Description).IsModified = true;
            }
            if (string.Equals(entity.Name, category.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                entry.Property(e => e.Name).IsModified = true;
            }
            entry.Property(e => e.Hash).IsModified = true;
            return entity.Code;
        }

        public int UpdateList(ICollection<Film.Domain.Enities.Category> entities)
        {
            var updBulk = new SqlParameter("Category_Update_Bulk_Value", SqlDbType.Structured);
            updBulk.Value = entities.ToDataTable();

            return _context.Database.ExecuteSqlRaw("EXEC Category_Upldate_Bulk @CategoryBulk",
                                                  new SqlParameter("@CategoryBulk", entities));

        }

        public async Task<int> UpdateListAsync(ICollection<Film.Domain.Enities.Category> entities)
        {
            var updBulk = new SqlParameter("Category_Update_Bulk_Value", SqlDbType.Structured);
            updBulk.Value = entities.ToDataTable();

            return await _context.Database.ExecuteSqlRawAsync("EXEC Category_Upldate_Bulk @CategoryBulk",
                                                    new SqlParameter("@CategoryBulk", entities));
        }
    }
}
