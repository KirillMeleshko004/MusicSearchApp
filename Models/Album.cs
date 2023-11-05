namespace MusicSearchApp.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }
        public int SongCount { get; set; }
        public string CoverImage { get; set; } = null!;

        public ICollection<Song> Songs { get; set; } = null!;
    }
}