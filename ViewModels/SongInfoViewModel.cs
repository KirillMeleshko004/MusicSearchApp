namespace MusicSearchApp.ViewModels
{
    public class SongInfoViewModel
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;

        public AlbumInfoViewModel Album { get; set; } = null!;
        public ProfileViewModel Artist { get; set; } = null!;

        public double Length { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string? GenreName { get; set; }

        public int ListenCount { get; set; }
        public string FilePath { get; set; } = null!;
    }
}