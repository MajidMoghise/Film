using Film.Domain.Contract.Base.Models;

namespace Film.Domain.Contract.Base.Repository
{
    public interface IBaseRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<BulkResponseModel> GetIdsFromHash(ICollection<BulkRequestModel> bulkRequests);
        Task<int> GetMaxId();

    }
}