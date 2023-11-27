using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class SubsciptionViewModel
    {
        public ProfileViewModel Subscriber { get; set; } = null!;
        public ProfileViewModel Artist { get; set; } = null!;
        
        public DateTime StartDate { get; set; }

        public SubsciptionViewModel(Subscription subscription)
        {
            Subscriber = new(subscription.Subscriber);
            Artist = new(subscription.Artist);
            StartDate = subscription.StartDate;
        }
    }
}