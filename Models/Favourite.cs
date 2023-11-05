namespace MusicSearchApp.Models
{
    public class Favourite
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime AdditionDate { get; set; }
    }
}