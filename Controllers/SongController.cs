using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{

    
    [Route("api/{controller}")]
    public class SongController : Controller
    {
        private readonly AlbumUploadingService _uploadingService;
        private readonly  MusicPlayService _playService;

        public SongController(AlbumUploadingService uploadingService, MusicPlayService playService)
        {
            _uploadingService = uploadingService;
            _playService = playService;
        }

        [HttpGet]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Play(int id)
        {
            return File((await _playService.Play(id))!, "audio/mpeg");
        }
        [HttpGet]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _playService.GetSong(id));
        }

        [HttpPost]
        [Route("{action}")]
        public async Task<IActionResult> Upload([FromForm]AlbumViewModel album)
        {
            await _uploadingService.UploadAlbum(album);
            return Ok(new {message = ModelState.IsValid});
        }
    }
}