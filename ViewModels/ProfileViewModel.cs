using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string DisplayedName { get; set; } = null!;
        public string? Description { get; set; }
        public string ProfileImage { get; set; } = null!;
        public bool IsBlocked { get; set; }
        public int SubscribersCount { get; set; }

        public ProfileViewModel(User user)
        {
            UserId = user.Id;
            UserName = user.UserName!;
            DisplayedName = user.DisplayedName!;
            ProfileImage = user.ProfileImage!;
            IsBlocked = user.IsBlocked!;
            SubscribersCount = user.SubscribersCount!;
            Role = user.Role!;
            Description = user.Description;
        }
        public ProfileViewModel()
        {
            
        }
    }
}