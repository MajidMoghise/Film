using Film.Domain.Contract.Base.Models;
using Film.Domain.Contract.Base.Repository;
using Film.Domain.Contract.Category.Models;

namespace Film.Domain.Contract.Category
{
    public interface ICategoryRepository : ICreateRepository<Enities.Category>, IUpdateRepository<Enities.Category>
    {
        Task<PageResponseModel<CategoriesListResponseModel>> GetListAsync(CategoriesListFilterRequestModel filter);
        Task<CategorDetailResponseModel> GetDetailAsync(int id);
        Task Delete(Domain.Enities.Category category);
    }

}