using Film.Application.Contract.Base.Dtos;
using Film.Application.Contract.UpLoad;
using Film.Application.Contract.UpLoad.Dtos;
using Film.WebAPI.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Film.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly FilmMapper _mapper;
        public UploadController()
        {
            _mapper = new FilmMapper();
        }
        [HttpPost(Name ="Upload")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> UploadFile(
            [FromForm] 
            UpLoadFileDto upLoad, 
            [FromServices]
            IUploadService _uploadService)
        {
            await _uploadService.UploadFile(upLoad);
            return Ok(new ResponseDto { Message="File Uploaded"});
        }
        [HttpGet(Name ="Sync")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(ResponseDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ResponseDto))]
        public async Task<IActionResult> SyncedFile(
            [FromForm] 
            SyncFromFileDto sync, 
            [FromServices]
            IUploadService _uploadService)
        {
            await _uploadService.SyncFromFileUploaded(sync);
            return Ok(new ResponseDto { Message="File Synced"});
        }

        
        }
}