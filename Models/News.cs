namespace MusicSearchApp.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public DateTime Date { get; set; }

        public int PublisherId { get; set; }
        public User Publisher { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;
    }
}