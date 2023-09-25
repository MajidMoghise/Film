using Film.Domain.Enities;

namespace Film.Domain.Contract.Base.Repository
{
    public interface IUpdateRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateListAsync(ICollection<TEntity> entities);
        int Update(TEntity entity);
        int UpdateList(ICollection<TEntity> entities);
    }
}