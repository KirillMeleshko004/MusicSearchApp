using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using MusicSearchApp.Models.Static;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class ProfileEditService
    {
        private const string defaultProfileImage = "Images/Profile/default_profile_img.svg";
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        private readonly ActionService _actionService;
        public ProfileEditService(UserManager<User> userManager, FileService fileService, 
            ActionService actionService)
        {
            _userManager = userManager;
            _fileService = fileService;
            _actionService = actionService;
        }

        private async Task<bool> IsChangeAllowed(int changedId, string actorName)
        {
            User actor = (await _userManager.FindByNameAsync(actorName))!;

            return actor.Id == changedId || actor.Role == UserRoles.Admin; 
        }

        public async Task<IResponse<ProfileViewModel>> GetByIdAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null) return new Response<ProfileViewModel>() 
                { Status = StatusCode.NotFound, Message = "User not found"};

            
            return new Response<ProfileViewModel>() 
                { Status = StatusCode.Ok, Message = "Success", Data = new(user) };
        }

        public async Task<IResponse<ProfileViewModel>> ChangeAsync(int id, string actorName, 
            string displayedName, string description, IFormFile? image)
        {
            Response<ProfileViewModel> response = new();

            if(!await IsChangeAllowed(id, actorName))
            {
                response.Status = StatusCode.Forbidden;
                response.Message = "Opreation forbidden";
                return response;
            }

            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null) 
            {
                response.Status = StatusCode.NotFound;
                response.Message = "User not found";
                return response;
            }
            
            user.DisplayedName = displayedName;
            user.Description = description;

            if(image != null) 
            {
                string? fileName = await _fileService.SaveFile(image, FileService.FileType.ProfileImage);
                if(fileName != null && fileName != defaultProfileImage)
                {
                    _fileService.DeleteFile(user.ProfileImage);
                    user.ProfileImage = fileName;
                } 
            }

            await _userManager.UpdateAsync(user);

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(user);

            await _actionService.CreateAction(actorName, 
                "Changed profile");

            return response;
        }
    }
}