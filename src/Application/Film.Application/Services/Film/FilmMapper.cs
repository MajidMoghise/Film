using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Film.Dtos;
using Film.Application.Helper;
using Film.Domain.Contract.Base.Models;
using Film.Domain.Contract.Film.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Services.Film
{
    internal class FilmMapper
    {
        internal Domain.Enities.Film Film(CreateFilmDto film)
        {
            return new Domain.Enities.Film
            {
                CategoryId = film.CategoryId,
                Description = film.Description,
                IsEnabled = film.IsEnabled,
                Name = film.Name,
                Code=film.Code
            };
        }

        internal Domain.Enities.Film Film(FilmDeleteRequestDto film)
        {
            return new Domain.Enities.Film
            {
                Code = film.Code,
                RowVersion = film.RowVersion,
            };
        }

        internal Domain.Enities.Film Film(FilmDto film)
        {
            return new Domain.Enities.Film
            {
                RowVersion = film.RowVersion,
                Code = film.Code,
                Name = film.Name,
                LastUpdate = film.LastUpdate,
                CategoryId = film.CategoryId,
                Description = film.Description,
                IsEnabled = film.IsEnabled
            };
        }

        internal FilmDetailResponseDto FilmDetailResponseDto(FilmDetailResponseModel resultModel)
        {
            return new FilmDetailResponseDto
            {
                LastUpdate = resultModel.LastUpdate,
                IsEnabled = resultModel.IsEnabled,
                Description = resultModel.Description,
                CategoryId = resultModel.CategoryId,
                CategoryName = resultModel.CategoryName,
                Hash = resultModel.Hash,
                Code = resultModel.Code,
                Name = resultModel.Name,
                RowVersion = resultModel.RowVersion
            };
        }

        internal FilmsListFilterRequestModel FilmsListFilterRequestModel(FilmsListFilterRequestDto resultModel)
        {
            return new FilmsListFilterRequestModel
            {
                IsEnabled = resultModel.IsEnabled,
                CategoryName = resultModel.CategoryName,
                Code = resultModel.Code,
                Name = resultModel.Name,
            };

        }

        internal PageResponseDto<FilmsListResponseDto> PageResponseDto_FilmsListResponseDto(PageResponseModel<FilmsListResponseModel> resultModel)
        {
            return new PageResponseDto<FilmsListResponseDto>
            {
                Data=resultModel.Data.Select(s=> FilmsListResponseDto(s)).ToList(),
                PageIndex=resultModel.PageIndex,
                PageSize=resultModel.PageSize,
                TotalCount=resultModel.TotalCount,  
                TotalPage = resultModel.TotalPage
            };
        }
        internal FilmsListResponseDto FilmsListResponseDto(FilmsListResponseModel resultModel)
        {
            return new FilmsListResponseDto
            {
                IsEnabled = resultModel.IsEnabled,
                CategoryId = resultModel.CategoryId,
                CategoryName = resultModel.CategoryName,
                Code = resultModel.Code,
                Name = resultModel.Name,
            };
        }

        internal ICollection<BulkRequestModel> List_BulkRequestModel(ICollection<CreateFilmDto> films)
        {
            return films.Select(s => BulkRequestModel(s)).ToList();
        }
        internal BulkRequestModel BulkRequestModel(CreateFilmDto film)
        {
            return new BulkRequestModel
            {
                Code = film.Code,
                Hash = film.GetSHA256()
            };
        }

        internal ICollection<Domain.Enities.Film> Film(ICollection<CreateFilmDto> films)
        {
            return films.Select(s => Film(s)).ToList();
        }
       
    }
}
