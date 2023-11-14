using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using MusicSearchApp.Models.Static;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class ProfileEditService
    {
        private const string defaultProfileImage = "Images/Profile/default_profile_img.svg";
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        public ProfileEditService(UserManager<User> userManager,FileService fileService)
        {
            _userManager = userManager;
            _fileService = fileService;
        }

        public async Task<bool> IsChangeAllowed(int changedId, string actorName)
        {
            User actor = (await _userManager.FindByNameAsync(actorName))!;

            return actor.Id == changedId || actor.Role == UserRoles.Admin; 

        }

        public async Task<bool> ChangeAsync(int id, string displayedName, string description, IFormFile? image)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null) return false;
            
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

            return true;
        }

        public async Task<ProfileViewModel?> DeleteAsync(string username)
        {
            User? user = await _userManager.FindByNameAsync(username);
            if(user == null) return null;

            await _userManager.DeleteAsync(user);
            return new ProfileViewModel(user);
        }

        // public IEnumerable<ProfileViewModel> Get(int start, int end)
        // {
        //     return _userManager.Users.SkipWhile(u => u.Id < start)
        //         .TakeWhile(u => u.Id <= end)
        //         .Select(u => new ProfileViewModel(u));
        // }

        public async Task<ProfileViewModel?> GetByIdAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            return user == null ? null : new ProfileViewModel(user);
        }
        public async Task<ProfileViewModel?> GetByUsernameAsync(string username)
        {
            User? user = await _userManager.FindByNameAsync(username);
            return user == null ? null : new ProfileViewModel(user);
        }
    }
}