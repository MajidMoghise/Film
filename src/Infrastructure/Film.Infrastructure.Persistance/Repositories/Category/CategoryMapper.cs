using Film.Domain.Contract.Category.Models;

namespace Film.Infrastructure.Persistance.Repositories.Film
{
    internal class CategoryMapper
    {
        internal Domain.Enities.Category Category(CategoryDeleteRequestModel del)
        {
            return new Domain.Enities.Category
            {
                Code = del.Code,
                RowVersion = del.RowVersion
            };
        }

        internal CategorDetailResponseModel CategoryDetailResponseModel(Domain.Enities.Category input)
        {
            return new CategorDetailResponseModel
            {
                Priority = input.Priority,
                Description = input.Description,
                Code = input.Code,
                IsEnabled = input.IsEnabled,
                LastUpdate = input.LastUpdate,
                Name = input.Name,
                RowVersion = input.RowVersion
            };
        }

        internal CategoriesListResponseModel CategorysListResponseModel(Domain.Enities.Category input)
        {
            return new CategoriesListResponseModel
            {
                Priority = input.Priority,
                Description = input.Description,
                Code = input.Code,
                IsEnabled = input.IsEnabled,
                Name = input.Name,
            };
        }
    }
}
