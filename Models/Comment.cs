namespace MusicSearchApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime Date { get; set; }
        
        public int SongId { get; set; }
        public Song Song { get; set; } = null!;

        public int UserID { get; set; }
        public User User { get; set; } = null!;

        public string Text { get; set; } = null!;
    }
}