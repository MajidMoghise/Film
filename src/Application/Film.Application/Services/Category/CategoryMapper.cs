using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category.Dtos;
using Film.Application.Contract.Film.Dtos;
using Film.Application.Helper;
using Film.Domain.Contract.Base.Models;
using Film.Domain.Contract.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Services.Category
{
    internal class CategoryMapper
    {
        internal CategoriesListFilterRequestModel CategoriesListFilterRequestModel(CategoriesListFilterRequestDto filter)
        {
            return new CategoriesListFilterRequestModel
            {
                IsEnabled = filter.IsEnabled,
                Name = filter.Name,
                OrderByAsces = filter.OrderByAsces,
                OrderByDesces = filter.OrderByDesces,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                Priority = filter.Priority,
            };
        }

        internal Domain.Enities.Category Category(CategoryDto input)
        {
            return new Domain.Enities.Category
            {
                Code = input.Code,
                RowVersion = input.RowVersion,
                Description=input.Description,
                IsEnabled=input.IsEnabled,
                LastUpdate=input.LastUpdate,
                Name=input.Name,
                Priority=input.Priority
            };
        }
        internal Domain.Enities.Category Category(CategoryDeleteRequestDto del)
        {
            return new Domain.Enities.Category
            {
                Code = del.Code,
                RowVersion = del.RowVersion
            };
        }
        internal Domain.Enities.Category Category(CreateCategoryDto input)
        {
            return new Domain.Enities.Category
            {
                Description = input.Description,
                IsEnabled = input.IsEnabled,
                Name = input.Name,
                Priority = input.Priority,
            };
        }

        internal CategoryDeleteRequestModel CategoryDeleteRequestModel(CategoryDeleteRequestDto category)
        {
            return new CategoryDeleteRequestModel
            {
                Code = category.Code,
                RowVersion = category.RowVersion
            };
        }

        internal PageResponseDto<CategoriesListResponseDto> PageResponseDto_CategoriesListResponseDto(PageResponseModel<CategoriesListResponseModel> resultModel)
        {
            return new PageResponseDto<CategoriesListResponseDto>
            {
                Data = resultModel.Data.Select(s => CategoriesListResponseDto(s)).ToList(),
                TotalCount=resultModel.TotalCount,
                TotalPage=resultModel.TotalPage,
                PageIndex= resultModel.PageIndex,
                PageSize = resultModel.PageSize,
            };
        }
        internal CategoriesListResponseDto CategoriesListResponseDto(CategoriesListResponseModel resultModel)
        {
            return new CategoriesListResponseDto
            {
                Description = resultModel.Description,
                Code = resultModel.Code,
                IsEnabled = resultModel.IsEnabled,
                Name = resultModel.Name,
                Priority = resultModel.Priority,
            };
        }

        internal CategorDetailResponseDto CategorDetailResponseDto(CategorDetailResponseModel resultModel)
        {
            return new CategorDetailResponseDto
            {
                Description = resultModel.Description,
                Code = resultModel.Code,
                IsEnabled = resultModel.IsEnabled,
                LastUpdate = resultModel.LastUpdate,
                Name = resultModel.Name,
                Priority = resultModel.Priority,
                RowVersion = resultModel.RowVersion,
            };
        }

        internal ICollection<BulkRequestModel> List_CategoryBulkRequestModel(ICollection<CreateCategoryDto> categories)
        {
            return categories.Select(s => BulkRequestModel(s)).ToList();
        }
        internal BulkRequestModel BulkRequestModel(CreateCategoryDto category)
        {
            return new BulkRequestModel
            {
                Code = category.Code,
                Hash = category.GetSHA256()
            };
        }

        internal ICollection<Domain.Enities.Category> List_Category(ICollection<CreateCategoryDto> categories)
        {
           return categories.Select(s=>Category(s)).ToList(); 
        }
        
    }
}
