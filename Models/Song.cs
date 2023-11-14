namespace MusicSearchApp.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;

        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;

        public int ArtistId { get; set; }
        public User Artist { get; set; } = null!;

        public double Length { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Lyrics { get; set; }

        public string? GenreName { get; set; }
        public Genre? Genre { get; set; }

        public bool Downloadable { get; set; }
        public int ListenCount { get; set; }
        public string FilePath { get; set; } = null!;
        public bool IsPublic { get; set; }

        #region Navigation properties

        public ICollection<Comment> Comments { get; set; } = null!;

        #endregion
    }
}