using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{

    
    [Route("api/{controller}")]
    public class AlbumController : Controller
    {
        private readonly AlbumUploadingService _uploadingService;
        private readonly  MusicPlayService _playService;
        private readonly MusicControlService _musicService;

        public AlbumController(AlbumUploadingService uploadingService, MusicPlayService playService,
            MusicControlService musicService)
        {
            _uploadingService = uploadingService;
            _playService = playService;
            _musicService = musicService;
        }

        [HttpPost]
        [Authorize]
        [Route("{action}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm]AlbumViewModel album)
        {
            if(!ModelState.IsValid) return BadRequest();
            
            IResponse<AlbumInfoViewModel> result = await _uploadingService.UploadAlbum(album);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }
            
            return Ok(new { album = result.Data, message = result.Message });
        }
        
        [HttpGet]
        [Route("{action}/{id}")]
        public IActionResult Get(int id)
        {
            IResponse<AlbumInfoViewModel> result = _playService.GetAlbum(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { album = result.Data, message = result.Message });
        }

        [HttpDelete]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            IResponse<AlbumInfoViewModel> result = await _musicService.DeleteAlbum(id, actorName);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { album = result.Data, message = result.Message });
        }

        [HttpGet]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> GetLibrary(int id)
        {
            IResponse<IEnumerable<AlbumInfoViewModel>> result = await _playService.GetLibrary(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { library = result.Data, message = result.Message });
        }
    }
}