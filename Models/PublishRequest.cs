namespace MusicSearchApp.Models
{
    public class PublishRequest
    {
        public int RequestId { get; set; }
        public DateTime Date { get; set; }

        public int ArtistId { get; set; }
        public User Artist { get; set; } = null!;

        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;

        public int StatusId { get; set; }
        public RequestStatus Status { get; set; } = null!;
    }
}