using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Services;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{
    // [Authorize]
    [Route("api/{controller}")]
    public class ProfileController : Controller
    {
        private readonly ProfileEditService _editService;
        private readonly FileService _fileService;

        public ProfileController(ProfileEditService editService, FileService fileService)
        {
            _editService = editService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            string username = ControllerContext.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            ProfileViewModel? profile = await _editService.GetByUsernameAsync(username);

            if(profile == null) return NotFound(new {errorMessage = "User not found"});
            
            return Ok(new { profile});
        }

        [HttpPatch]
        [Route("change")]
        [Authorize]
        public async Task<IActionResult> Change(string displayedName, string description, IFormFile image)
        {
            string username = ControllerContext.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            bool res = await _editService.ChangeAsync(displayedName, description, image, username);
            if(!res) return NotFound(new {errorMessage = "Error"});

            return Ok(new {message = "changed"});
        } 
    }
}