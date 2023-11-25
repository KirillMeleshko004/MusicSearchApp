using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public DateTime Date { get; set; }

        public ProfileViewModel Artist { get; set; } = null!;

        public AlbumInfoViewModel Album { get; set; } = null!;

        public string Status { get; set; } = null!;

        public RequestViewModel(PublishRequest request)
        {
            RequestId = request.RequestId;
            Date = request.Date;
            Artist = new(request.Artist);
            Album = new(request.Album);
            Status = request.Status.Status;
        }
    }
}