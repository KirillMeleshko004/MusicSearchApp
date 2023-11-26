using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult Get([FromQuery]int start, [FromQuery]int end)
        {  
            if(!ModelState.IsValid) return BadRequest();
            
            IResponse<IEnumerable<NewsViewModel>> result = 
                _newsService.GetNews(start, end);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new{news = result.Data, message = result.Message });
        }
    }
}