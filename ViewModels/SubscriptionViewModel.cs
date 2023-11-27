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
            if(subscription.Subscriber != null)
                Subscriber = new(subscription.Subscriber);
            if(subscription.Artist != null)
                Artist = new(subscription.Artist);
            StartDate = subscription.StartDate;
        }
    }
}