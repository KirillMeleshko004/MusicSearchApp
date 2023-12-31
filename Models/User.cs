using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace MusicSearchApp.Models
{
    public class User : IdentityUser<int>
    {
        [MaxLength(30)]
        public string DisplayedName { get; set; } = null!;
        
        [MaxLength(500)]
        public string? Description { get; set; }

        public string ProfileImage { get; set; } = null!;
        public string? Role { get; set; }
        public int SubscribersCount { get; set; }
        public bool IsBlocked { get; set; }

        
        #region Navigation properties

        public ICollection<Song> Songs { get; set; } = null!;
        public ICollection<Album> Albums { get; set; } = null!;
        public ICollection<PublishRequest> Requests { get; set; } = null!;
        public ICollection<Subscription> Subscriptions { get; set; } = null!;
        public ICollection<Subscription> Subsribers { get; set; } = null!;
        public ICollection<Action> Actions { get; set; } = null!;

        #endregion
    }
}