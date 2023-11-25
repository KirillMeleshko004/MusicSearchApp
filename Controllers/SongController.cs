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
            
            IResponse<SongInfoViewModel> result = _playService.GetSong(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { song = result.Data, message = result.Message });
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult Get([FromQuery]string searchString)
        {
            IResponse<IEnumerable<SongInfoViewModel>> result = _playService.GetSongs(searchString);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { songs = result.Data, message = result.Message });
        }
    }
}