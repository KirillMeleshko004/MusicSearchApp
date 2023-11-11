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

        public async Task<ProfileViewModel?> ChangeAsync(ProfileViewModel data, int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null) return null;

            
            user.DisplayedName = data.DisplayedName;
            user.Description = data.Description;
            user.ProfileImage = data.ProfileImage;
            user.SubscribersCount = data.SubscribersCount;
            user.IsBlocked = data.IsBlocked;
            
            await _userManager.UpdateAsync(user);

            return data;
        }

        public async Task<ProfileViewModel?> DeleteAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null) return null;

            await _userManager.DeleteAsync(user);
            return new ProfileViewModel(user);
        }

        public IEnumerable<ProfileViewModel> Get(int start, int end)
        {
            return _userManager.Users.SkipWhile(u => u.Id < start)
                .TakeWhile(u => u.Id <= end)
                .Select(u => new ProfileViewModel(u));
        }

        public async Task<ProfileViewModel?> GetByIdAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            return user == null ? null : new ProfileViewModel(user);
        }
    }
}