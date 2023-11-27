using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Subscribe(int id, [FromQuery]int subscriberId)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<SubsciptionViewModel> result = 
                await _subscriptionService.SubscribeAsync(subscriberId, id);

            if(result.Status != Services.Interfaces.StatusCode.Created)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { subscription = result.Data, message = result.Message });
        }

        [HttpDelete]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> Unsubscribe(int id, [FromQuery]int subscriberId)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<SubsciptionViewModel> result = 
                await _subscriptionService.UnsubscribeAsync(subscriberId, id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { subscription = result.Data, message = result.Message });
        }

        [HttpGet]
        [Authorize]
        [Route("{action}/{id}")]
        public async Task<IActionResult> GetSubscribed(int id, [FromQuery]int subscriberId)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<bool> result = 
                await _subscriptionService.IsSubscribedAsync(subscriberId, id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { isSubscribed = result.Data, message = result.Message });
        }
    }
}