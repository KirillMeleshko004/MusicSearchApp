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
        public IActionResult Get(int id)
        {
            
            SongInfoViewModel? albumInfo = _playService.GetSong(id);

            if(albumInfo == null) return BadRequest();

            return Ok(albumInfo);
        }

        [HttpGet]
        [Route("{action}/{page}")]
        public IActionResult GetSongs(int page, [FromQuery]string searchString)
        {
            System.Console.WriteLine(searchString);

            return Ok(new{songs = _playService.GetSongs(page, searchString)});
        }


        [HttpGet]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> GetLibrary(int id)
        {
            IEnumerable<AlbumInfoViewModel>? library = await _playService.GetLibrary(id);
            if(library == null) return BadRequest();

            return Ok(new{library});
        }
    }
}