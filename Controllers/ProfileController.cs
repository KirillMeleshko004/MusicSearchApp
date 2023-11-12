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
        public async Task<IActionResult> GetUser()
        {
            string username = ControllerContext.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            ProfileViewModel? profile = await _editService.GetByUsernameAsync(username);

            if(profile == null) return NotFound(new {errorMessage = "User not found"});
            
            return Ok(new { profile});
        }

        [HttpPatch]
        [Route("change")]
        public async Task<IActionResult> Change(string displayedName, string description)
        {
            string username = ControllerContext.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            bool res = await _editService.ChangeAsync(displayedName, description, username);
            if(!res) return NotFound(new {errorMessage = "Error"});

            return Ok();
        } 

        [HttpPatch]
        [Route("changeicon")]
        public async Task<IActionResult> ChangeIcon(IFormFile image)
        {
            System.Console.WriteLine(ControllerContext.HttpContext.Request.Form.Files[0].FileName);
            
            string? fileName = await _fileService.SaveFile(image);

            if(fileName == null) return StatusCode(StatusCodes.Status500InternalServerError, new {errorMessage = "Error"});

            return Ok(new {filename = fileName});
        } 

    }
}