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
        [Route("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            ProfileViewModel? profile = await _editService.GetByIdAsync(id);

            if(profile == null) return NotFound(new {errorMessage = "User not found"});
            
            return Ok(new { profile});
        }

        [HttpPatch]
        [Route("change/{id}")]
        [Authorize]
        public async Task<IActionResult> Change(int id, string displayedName, string description, IFormFile image)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            bool isAllowed = await _editService.IsChangeAllowed(id, actorName);
            if(!isAllowed)
            {
                return Forbid();
            }
            
            bool res = await _editService.ChangeAsync(id, displayedName, description, image);
            if(!res) return NotFound();

            return Ok(new {message = "changed"});
        } 
    }
}