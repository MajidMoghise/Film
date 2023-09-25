using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.Category
{
    public interface ICategoryService
    {
        Task<PageResponseDto<CategoriesListResponseDto>> GetCategories(CategoriesListFilterRequestDto filter);
        Task<CategorDetailResponseDto> GetDetailCategory(int categoryId);
        Task<int> UpdateCategory(CategoryDto category);
        Task DeleteCategory(CategoryDeleteRequestDto category);
        Task<int> CreateCategory(CreateCategoryDto category);
        Task BulkCategories(ICollection<CreateCategoryDto> categories);
        Task BulkUpdateCategories(ICollection<CreateCategoryDto> categories);
        Task BulkInsertCategories(ICollection<CreateCategoryDto> categories);

    }
}
