using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.Category;
using Film.Application.Contract.Category.Dtos;
using Film.WebAPI.Mapper;
using Film.WebAPI.ViewModels.FilmController;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Film.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly FilmMapper _mapper;
        
        public CategoryController()
        {
            _mapper = new FilmMapper();
        }

        [HttpGet(Name = "ListCategories")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(GeneralResponseDto<PageResponseDto<CategoriesListResponseDto>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> GetCategories(
            [FromQuery]
            CategoriesListFilterRequestDto filter,
            [FromServices]
            ICategoryService _CategoryService
            )
        {
            var result = await _CategoryService.GetCategories(filter);
            return Ok(new GeneralResponseDto<PageResponseDto<CategoriesListResponseDto>> { Data = result });
        }

        [HttpGet("{id}", Name = "DetailCategory")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(GeneralResponseDto<CategorDetailResponseDto>))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> GetDetailCategory(
            [FromRoute]
            int Id,
            [FromServices]
            ICategoryService _CategoryService
            )
        {
            var result = await _CategoryService.GetDetailCategory(Id);
            return Ok(new GeneralResponseDto<CategorDetailResponseDto> { Data = result });
        }

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> CreateCategory(
            [FromBody]
            CreateCategoryDto Category,
            [FromServices]
            ICategoryService _CategoryService
            )
        {
            var result = await _CategoryService.CreateCategory(Category);
            return Ok(new ResponseDto
            {
                Message = "Created",
                Url = Request.Path + "/" + result.ToString()
            });
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        [ProducesResponseType(statusCode: StatusCodes.Status202Accepted, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute]
            [Required]
            int id,
            [FromBody]
            CategoryUpdateRequestVM category,
            [FromServices] ICategoryService _CategoryService)
        {
            var categoryupd = _mapper.CategoryDto(category, id);
            var result = await _CategoryService.UpdateCategory(categoryupd);
            return Ok(new ResponseDto
            {
                Message = "updated",
                Url = Request.Path + "/" + result.ToString()
            });

        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(statusCode: StatusCodes.Status202Accepted, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(ResponseDto))]
        public async Task<IActionResult> DeleteCategory(
            [FromRoute]
            [Required] int id,
            [FromBody] DeleteCategoryVM category,
            [FromServices] ICategoryService _CategoryService
            )
        {
            var delete = _mapper.CategoryDeleteRequestDto(category, id);
             await _CategoryService.DeleteCategory(delete);
            return Ok(new ResponseDto { Message = "deleted"});
        }

    }
}