namespace MusicSearchApp.Models
{
    public class News
    {
        public DateTime Date { get; set; }
        public int PublisherId { get; set; }
        public string Content { get; set; } = null!;
        public int SongId { get; set; }
    }
}