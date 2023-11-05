namespace MusicSearchApp.Models
{
    public class Subscription
    {
        public int SubscriberId { get; set; }
        public int ArtistId { get; set; }
        public DateTime StartDate { get; set; }
        public long SubscriberNumber { get; set; }
    }
}