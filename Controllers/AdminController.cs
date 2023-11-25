using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<User> _userManager;
        private readonly AdminService _adminService;

        public AdminController(ApplicationContext context, UserManager<User> userManager,
            AdminService adminService)
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
        }


        [HttpGet]
        [Route("users/{action}")]
        [Authorize]
        public IActionResult Find([FromQuery]string username)
        {
            
            IResponse<IEnumerable<ProfileViewModel>> result = _adminService.GetUsers(username);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { users = result.Data, message = result.Message });
        }

        [HttpGet]
        [Route("users/{action}/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            IResponse<ProfileViewModel> result = await _adminService.GetByIdAsync(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { user = result.Data, message = result.Message });
        }

        [HttpPatch]
        [Route("users/{action}/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeBlock(int id)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            IResponse<ProfileViewModel> result = await _adminService.ChangeBlockAsync(id, actorName);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { profile = result.Data, message = result.Message });
        }
    }
}