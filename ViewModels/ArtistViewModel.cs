using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class ArtistViewModel
    {
        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public string DisplayedName { get; set; } = null!;
        public string? Description { get; set; }
        public string ProfileImage { get; set; } = null!;
        public bool IsBlocked { get; set; }
        public int SubscribersCount { get; set; }
        public IEnumerable<AlbumInfoViewModel>? Albums { get; set; }

        public ArtistViewModel(User user)
        {
            UserId = user.Id;
            DisplayedName = user.DisplayedName!;
            ProfileImage = user.ProfileImage!;
            IsBlocked = user.IsBlocked!;
            SubscribersCount = user.SubscribersCount!;
            Role = user.Role!;
            Description = user.Description;
        }
        public ArtistViewModel()
        {
            
        }
    }
}