using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category.Dtos;
using Film.Application.Contract.Film;
using Film.Application.Contract.Film.Dtos;
using Film.WebAPI.Mapper;
using Film.WebAPI.ViewModels.FilmController;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Film.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly FilmMapper _mapper;
        public FilmController()
        {
            _mapper = new FilmMapper();
        }

        [HttpGet(Name = "ListFilms")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(GeneralResponseDto<PageResponseDto<FilmsListResponseDto>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> GetFilms(
            [FromQuery]
            FilmsListFilterRequestDto filter,
            [FromServices]
            IFilmService _filmService)
        {
            var result = await _filmService.GetFilms(filter);
            return Ok(result);
        }

        [HttpGet("{id}",Name = "DetailFilm")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(GeneralResponseDto<FilmDetailResponseDto>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
       public async Task<IActionResult> GetDetailFilm(
            [FromRoute] 
            int Id, 
            [FromServices] 
            IFilmService _filmService
            )
        {
            var result=await _filmService.GetDetailFilm(Id);
            return Ok(new GeneralResponseDto<FilmDetailResponseDto> { Data = result });
        }

        [HttpPost(Name ="CreateFilm")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> CreateFilm(
            [FromBody] 
            CreateFilmDto film, 
            [FromServices] 
            IFilmService _filmService)
        {
            var result =await _filmService.CreateFilm(film);
            return Ok(new ResponseDto
            {
                Message = "Created",
                Url = Request.Path + "/" + result.ToString()
            });
        }

        [HttpPut("{id}",Name ="UpdateFilm")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        public async Task<IActionResult> UpdateFilm(
            [FromRoute]
            int id,
            [FromBody]
            FilmUpdateRequestVM film, 
            [FromServices] 
            IFilmService _filmService)
        {
            var upd = _mapper.FilmDto(film, id);
            var result =await _filmService.UpdateFilm(upd);
            return Ok(new ResponseDto
            {
                Message = "updated",
                Url = Request.Path + "/" + result.ToString()
            });

        }

        [HttpDelete("{id}",Name = "DeleteFilm")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        public async Task<IActionResult> DeleteFilm(
            [FromRoute]
            [Required]
            int id,
            [FromBody]
            DeleteFilmRequestVM film, 
            [FromServices] 
            IFilmService _filmService
            )
        {
            var delete = _mapper.FilmDeleteRequestDto(film,id);
            await _filmService.DeleteFilm(delete);
            return Ok(new ResponseDto { Message="Deleted"});
        }

       
        }
}