namespace MusicSearchApp.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;

        public int ArtistId { get; set; }
        public User Artist { get; set; } = null!;

        public int SongCount { get; set; }
        public string CoverImage { get; set; } = null!;


        #region Navigation properties

        public PublishRequest? Request { get; set; }
        public News? PublishNews { get; set; }
        public ICollection<Song> Songs { get; set; } = null!;

        #endregion
    }
}