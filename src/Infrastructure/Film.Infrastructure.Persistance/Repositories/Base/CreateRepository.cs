using Film.Domain.Enities;
using Film.Domain.Contract.Base.Repository;
using Microsoft.EntityFrameworkCore;
using Film.Infrastructure.Persistance.Context;


namespace Film.Infrastructure.Persistance.Repositories.Base
{
    public class CreateRepository<TEntity> :BaseRepository<TEntity>,  ICreateRepository<TEntity> where TEntity : BaseEntity
    {
        public CreateRepository(FilmDbContext context,IUnitOfWork unitOfWork):base(context,unitOfWork)
        {}

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            if(entity.Code==0)
            {
                entity.Code = await GetId();
            }
           await _entity.AddAsync(entity);
        }

        public void AddList(ICollection<TEntity> entities)
        {
            _entity.AddRange(entities);
        }

        public async Task AddListAsync(ICollection<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);    
        }

        public Task<int> GetId()
        {
            throw new NotImplementedException();
        }
    }
}