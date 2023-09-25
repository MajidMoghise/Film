using Film.Application.Contract.Category.Dtos;
using Film.Application.Contract.Film.Dtos;
using Film.WebAPI.ViewModels.FilmController;

namespace Film.WebAPI.Mapper
{
    internal class FilmMapper
    {
        internal CategoryDeleteRequestDto CategoryDeleteRequestDto(DeleteCategoryVM input, int id)
        {
            return new CategoryDeleteRequestDto();
        }
        internal CategoryDto CategoryDto(CategoryUpdateRequestVM input, int id)
        {
            return new CategoryDto();
        }
        internal FilmDeleteRequestDto FilmDeleteRequestDto(DeleteFilmRequestVM input, int id)
        {
            return new FilmDeleteRequestDto();
        }

        internal FilmDto FilmDto(FilmUpdateRequestVM film, int id)
        {
            return new FilmDto();
        }
    }
}