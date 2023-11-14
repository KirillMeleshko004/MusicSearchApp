using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("users/{action}/{username}")]
        [Authorize]
        public IActionResult Find(string username)
        {
            IEnumerable<ProfileViewModel> users = _userManager.Users
                .Where(u => u.UserName!.ToLower().Contains(username.ToLower()))
                .Select(u => new ProfileViewModel(u));

            return Ok(users);
        }

        [HttpGet]
        [Route("users/{action}/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null) return NotFound(new {errorMessage = "User not found"});
            
            return Ok(new { user = new ProfileViewModel(user)});
        }

        [HttpPatch]
        [Route("users/{action}/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeBlock(int id)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(actorName == user!.UserName) return BadRequest(new {errorMessage = "You can't block yourself"});

            if(user == null) return NotFound(new {errorMessage = "User not found"});

            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);

            return Ok(new { message = "success"});
        }
    }
}