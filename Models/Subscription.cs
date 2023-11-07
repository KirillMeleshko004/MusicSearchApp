using System.ComponentModel.DataAnnotations;

namespace MusicSearchApp.Models
{
    public class Subscription
    {
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; } = null!;

        public int ArtistId { get; set; }
        public User Artist { get; set; } = null!;
        
        public DateTime StartDate { get; set; }
        public int SubscriberNumber { get; set; }
    }
}