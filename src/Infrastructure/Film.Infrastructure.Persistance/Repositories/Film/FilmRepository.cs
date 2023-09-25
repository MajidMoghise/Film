using Film.Domain.Contract.Base.Models;
using Film.Domain.Contract.Base.Repository;
using Film.Domain.Contract.Film;
using Film.Domain.Contract.Film.Models;
using Film.Domain.Enities;
using Film.Infrastructure.Persistance.Context;
using Film.Infrastructure.Persistance.Extentions;
using Film.Infrastructure.Persistance.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Repositories.Film
{
    public class FilmRepository : CreateRepository<Domain.Enities.Film>, IFilmRepository
    {
        private readonly FilmMapper _mapper;
        public FilmRepository(FilmDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _mapper = new FilmMapper();
        }

        public async Task Delete(Domain.Enities.Film del)
        {
            await Task.FromResult(_entity.Remove(del));

        }

        public async Task<FilmDetailResponseModel> GetDetailAsync(int code)
        {
            return await _entity.Select(_mapper.FilmDetailResponseModel)
                                .FirstOrDefaultAsync(e => e.Code == code);

        }

        
        public async Task<Domain.Contract.Base.Models.PageResponseModel<FilmsListResponseModel>> GetListAsync(FilmsListFilterRequestModel filter)
        {
            var predicate = Predicate(filter);

            var query = _entity.Where(predicate).AsQueryable();

            return await query.SortDynamic(filter.OrderByAsces, filter.OrderByDesces)
            .Select(s => _mapper.FilmsListResponseModel(s))
                         .PagingAsync(filter.PageSize, filter.PageIndex);

        }

        public int Update(Domain.Enities.Film entity)
        {
            var updQuery = $"Update Film set {nameof(Domain.Enities.Film.CategoryId)}={entity.CategoryId}," +
                $"{nameof(Domain.Enities.Film.Name)}='{entity.Name}'," +
                $"{nameof(Domain.Enities.Film.Description)}='{entity.Description}'," +
                $"{nameof(Domain.Enities.Film.IsEnabled)}={entity.IsEnabled}," +
                $"{nameof(Domain.Enities.Film.LastUpdate)}=GetDate()," +
                $"{nameof(Domain.Enities.Film.Hash)}={entity.Hash}" +
                $"Where {nameof(Domain.Enities.Film.Code)}={entity.Code} and {nameof(Domain.Enities.Film.RowVersion)}={entity.RowVersion}";
            return _context.Database.ExecuteSqlRaw(updQuery);
        }

        public async Task<int> UpdateAsync(Domain.Enities.Film entity)
        {
            var film = await _entity.FirstOrDefaultAsync(e => e.Code == entity.Code);
            if (film is null)
            {
                return 0;
            }
            var entry = _context.Entry(entity);
            if (entity.IsEnabled != film.IsEnabled)
            {
                entry.Property(e => e.IsEnabled).IsModified = true;
            }
            if (entity.CategoryId != film.CategoryId)
            {
                entry.Property(e => e.CategoryId).IsModified = true;
            }
            if (string.Equals(entity.Description, film.Description, StringComparison.CurrentCultureIgnoreCase))
            {
                entry.Property(e => e.Description).IsModified = true;
            }
            if (string.Equals(entity.Name, film.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                entry.Property(e => e.Name).IsModified = true;
            }
            entry.Property(e => e.Hash).IsModified = true;

            return 1;
        }

        public int UpdateList(ICollection<Domain.Enities.Film> entities)
        {
            var updBulk = new SqlParameter("Film_Update_Bulk_Value", SqlDbType.Structured);
            updBulk.Value = entities.ToDataTable();

            return _context.Database.ExecuteSqlRaw("EXEC Film_Upldate_Bulk @FilmBulk",
                                                  new SqlParameter("@FilmBulk", entities));

        }

        public async Task<int> UpdateListAsync(ICollection<Domain.Enities.Film> entities)
        {
            var updBulk = new SqlParameter("Film_Update_Bulk_Value", SqlDbType.Structured);
            updBulk.Value = entities.ToDataTable();

            return await _context.Database.ExecuteSqlRawAsync("EXEC Film_Upldate_Bulk @FilmBulk",
                                                    new SqlParameter("@FilmBulk", entities));
        }
    }
}
