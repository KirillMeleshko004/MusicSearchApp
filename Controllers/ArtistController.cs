using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class ArtistController : Controller
    {
        private readonly ArtistService _artistService;
        private readonly SubscriptionService _subscriptionService;

        public ArtistController(ArtistService artistService, SubscriptionService subscriptionService)
        {
            _artistService = artistService;
            _subscriptionService = subscriptionService;
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
    
        [HttpPost]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Subscribe(int id, [FromQuery]int userId)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<SubsciptionViewModel> result = await _subscriptionService.SubscribeAsync(userId, id);

            if(result.Status != Services.Interfaces.StatusCode.Created)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { artist = result.Data, message = result.Message });
        }

        [HttpDelete]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Unsubscribe(int id, [FromQuery]int userId)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<SubsciptionViewModel> result = await _subscriptionService.UnsubscribeAsync(userId, id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { artist = result.Data, message = result.Message });
        }
    }
}