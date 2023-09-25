using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Film.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Application.Contract.Film
{
    public interface IFilmService
    {
        Task<PageResponseDto<FilmsListResponseDto>> GetFilms(FilmsListFilterRequestDto filter);
        Task<FilmDetailResponseDto> GetDetailFilm(int filmId);
        Task<int> UpdateFilm(FilmDto film);
        Task DeleteFilm(FilmDeleteRequestDto film);
        Task<int> CreateFilm(CreateFilmDto film);
        Task BulkFilms(ICollection<CreateFilmDto> films);
        Task BulkUpdateFilms(ICollection<CreateFilmDto> films);
        Task BulkInsertFilms(ICollection<CreateFilmDto> films);
    }
}
