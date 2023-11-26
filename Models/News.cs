namespace MusicSearchApp.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public DateTime Date { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;
    }
}