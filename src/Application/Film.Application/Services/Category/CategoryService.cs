using Film.Application.Base;
using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category;
using Film.Application.Contract.Category.Dtos;
using Film.Application.Contract.Film.Dtos;
using Film.Application.Helper;
using Film.Domain.Contract.Category;
using Film.Domain.Contract.Film;
using Film.Domain.Enities;

namespace Film.Application.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = new CategoryMapper();
        }

        public async Task<int> CreateCategory(CreateCategoryDto category)
        {
            var cat = _mapper.Category(category);
            cat.Hash = cat.GetSHA256();

            await _categoryRepository.AddAsync(cat);
            _categoryRepository.UnitOfWork.Commit();
            if (cat.Code == 0) { throw new BusinessException("Create not succeed", BusinessExceptionType.None); }
            return cat.Code;
        }

        public async Task DeleteCategory(CategoryDeleteRequestDto category)
        {
            var del = _mapper.Category(category);
            await _categoryRepository.Delete(del);
            _categoryRepository.UnitOfWork.Commit();
            if (del.Code != 0) { throw new BusinessException("delete not succeed", BusinessExceptionType.None); }
            
        }

        public async Task<PageResponseDto<CategoriesListResponseDto>> GetCategories(CategoriesListFilterRequestDto filter)
        {
            var filtermodel = _mapper.CategoriesListFilterRequestModel(filter);
            var resultModel=await _categoryRepository.GetListAsync(filtermodel);
            return _mapper.PageResponseDto_CategoriesListResponseDto(resultModel);
        }

        public async Task<CategorDetailResponseDto> GetDetailCategory(int categoryId)
        {
            var resultModel = await _categoryRepository.GetDetailAsync(categoryId);
            return _mapper.CategorDetailResponseDto(resultModel);
        }

        public async Task<int> UpdateCategory(CategoryDto category)
        {
            var upd = _mapper.Category(category);
            await _categoryRepository.UpdateAsync(upd);
            _categoryRepository.UnitOfWork.Commit();
            if (upd.Code != 0) { throw new BusinessException("update not succeed", BusinessExceptionType.None); }
        return upd.Code;
        }
        public async Task BulkCategories(ICollection<CreateCategoryDto> categories)
        {
            var bulkRequests = _mapper.List_CategoryBulkRequestModel(categories);
            var resultBulk = await _categoryRepository.GetIdsFromHash(bulkRequests);
            await Task.WhenAll(
                Task.Run(async () =>
                {
                    var resultUpd = await SetUpdateCategoryFromExcel(categories, resultBulk.UpdateCodes);
                    await BulkUpdateCategories(resultUpd);
                }),
              Task.Run(async () =>
              {
                  var resultUpd = await SetUpdateCategoryFromExcel(categories, resultBulk.InsertCodes);
                  await BulkInsertCategories(resultUpd);
              }));
        }

        public async Task BulkUpdateCategories(ICollection<CreateCategoryDto> categories)
        {
            var upds = _mapper.List_Category(categories);
            await _categoryRepository.UpdateListAsync(upds);
        }

        public async Task BulkInsertCategories(ICollection<CreateCategoryDto> categories)
        {
            var inserts = _mapper.List_Category(categories);
            await _categoryRepository.AddListAsync(inserts);
        }
        private async Task<ICollection<CreateCategoryDto>> SetUpdateCategoryFromExcel(ICollection<CreateCategoryDto> Categories, List<int> codes)
        {
            return await Task.Run(() => Categories.Where(w => codes.Contains(w.Code)).ToList());
        }

    }
}
