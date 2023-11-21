using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{

    
    [Route("api/{controller}")]
    public class AlbumController : Controller
    {
        private readonly AlbumUploadingService _uploadingService;
        private readonly  MusicPlayService _playService;

        public AlbumController(AlbumUploadingService uploadingService, MusicPlayService playService)
        {
            _uploadingService = uploadingService;
            _playService = playService;
        }

        [HttpPost]
        [Authorize]
        [Route("{action}")]
        public async Task<IActionResult> Upload([FromForm]AlbumViewModel album)
        {
            await _uploadingService.UploadAlbum(album);
            return Ok(new {message = ModelState.IsValid});
        }
        
        [HttpGet]
        [Route("{action}/{id}")]
        public IActionResult Get(int id)
        {
            AlbumInfoViewModel? albumInfo = _playService.GetAlbum(id);

            if(albumInfo == null) return BadRequest();

            return Ok(albumInfo);
        }

        [HttpGet]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> GetLibrary(int id)
        {
            IEnumerable<AlbumInfoViewModel>? library = await _playService.GetLibrary(id);
            if(library == null) return BadRequest();

            return Ok(library);
        }
    }
}