using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class ProfileEditService
    {
        private readonly UserManager<User> _userManager;
        public ProfileEditService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ChangeAsync(string displayedName, string description, string username)
        {
            User? user = await _userManager.FindByNameAsync(username);
            if(user == null) return false;
            
            user.DisplayedName = displayedName;
            user.Description = description;
            
            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> ChangeIconAsync(string username, FileInfo icon)
        {
            User? user = await _userManager.FindByNameAsync(username);
            if(user == null) return false;
            

            
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
    }
}