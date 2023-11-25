using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.Static;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;
using NAudio.CoreAudioApi;

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
            IResponse<ProfileViewModel> result = await _editService.GetByIdAsync(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { profile = result.Data, message = result.Message });
        }

        [HttpPatch]
        [Route("change/{id}")]
        [Authorize]
        public async Task<IActionResult> Change(int id, string displayedName, string description, IFormFile image)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            IResponse<ProfileViewModel> result = 
                await _editService.ChangeAsync(id, actorName, displayedName, description, image);

            
            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { profile = result.Data, message = result.Message });
        } 
    }
}