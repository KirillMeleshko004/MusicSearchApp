namespace MusicSearchApp.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public double Length { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Lyrics { get; set; }
        public string Genre { get; set; } = null!;
        public bool Downloadable { get; set; }
        public int ListenCount { get; set; }
        public string FileName { get; set; } = null!;
        public bool IsPublic { get; set; }

        
        public ICollection<Comment> Comments { get; set; } = null!;
        public ICollection<Favourite> Favourites { get; set; } = null!;
    }
}