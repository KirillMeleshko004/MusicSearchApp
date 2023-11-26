using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class ArtistController : Controller
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Route("{action}/{id}")]
        public IActionResult Get(int id)
        {
            IResponse<ArtistViewModel> result = _artistService.GetArtist(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { artist = result.Data, message = result.Message });
        }
    }
}