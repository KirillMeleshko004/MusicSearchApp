namespace MusicSearchApp.Models
{
    public class Comment
    {
        public DateTime Date { get; set; }
        public int SongId { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; } = null!;
    }
}