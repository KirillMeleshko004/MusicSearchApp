namespace MusicSearchApp.ViewModels
{
    public class AlbumInfoViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public bool IsPublic { get; set; }
        public bool Downloadable { get; set; }

        public ProfileViewModel Artist { get; set; } = null!;

        public int SongCount { get; set; }
        public string CoverImage { get; set; } = null!;


        public ICollection<SongInfoViewModel> Songs { get; set; } = null!;
    }
}