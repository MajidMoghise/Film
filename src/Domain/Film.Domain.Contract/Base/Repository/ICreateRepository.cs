using Film.Domain.Enities;

namespace Film.Domain.Contract.Base.Repository
{
    public interface ICreateRepository<TEntity>: IBaseRepository where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task AddListAsync(ICollection<TEntity> entities);
        void Add(TEntity entity);
        void AddList(ICollection<TEntity> entities);
    }}