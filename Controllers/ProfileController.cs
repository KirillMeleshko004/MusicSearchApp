using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Models.Repos.Interfaces;
using MusicSearchApp.Services;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Controllers
{
    [Authorize]
    [Route("{controller}")]
    public class ProfileController : Controller
    {
        private readonly ProfileEditService _editService;

        public ProfileController(ProfileEditService editService)
        {
            _editService = editService;
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<IActionResult> GetUser(int id)
        {
            ProfileViewModel? profile = await _editService.GetByIdAsync(id);

            if(profile == null) return NotFound(new {errorMessage = "User not found"});
            
            return Ok(new 
                {
                    userId = profile.UserId, 
                    username = profile.UserName,
                    role = profile.Role,
                    displayedName = profile.DisplayedName,
                    //To change
                    profileImage = profile.ProfileImage,
                    //
                    idBlocked = profile.IsBlocked,
                    subscribersCount = profile.SubscribersCount,
                }
            );
        }

        [HttpPatch]
        public IActionResult Change(string displayedName, string description)
        {
            // string username = ControllerContext.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            // User? user = await _userManager.FindByNameAsync(username);
            // if(user == null) return NotFound(new {errorMessage = "User not found"});

            // var res = await _userManager.UpdateAsync(user);
            // if(!res.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        } 

    }
}