using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicSearchApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; } = null!;

        [MaxLength(30)]
        public string Password { get; set; } = null!;
        
        [MaxLength(300)]
        public string? Description { get; set; }

        
        [MaxLength(300)]
        public string? ImgUrl { get; set; }
        public int SubscriptionsCount { get; set; }

        [MaxLength(10)]
        public string Role { get; set; } = null!;
        public bool IsBlocked { get; set; }

        

        public ICollection<Song> Songs { get; set; } = null!;
        public ICollection<Album> Albums { get; set; } = null!;
        public ICollection<Favourite> Favourites { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = null!;
        public ICollection<PublishRequest> Requests { get; set; } = null!;
        public ICollection<News> News { get; set; } = null!;
        public ICollection<Subscription> Subscriptions { get; set; } = null!;
        public ICollection<Subscription> Subsribers { get; set; } = null!;
        public ICollection<Action> Actions { get; set; } = null!;
    }
}