namespace MusicSearchApp.Models
{
    public class PublishRequest
    {
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public long SongId { get; set; }
        public int AlbumId { get; set; }
    }
}