namespace MusicSearchApp.ViewModels
{
    public class AlbumViewModel
    {
        public int ArtistId { get; set; }
        public string AlbumTitle { get; set; } = null!;
        public IFormFile CoverImage { get; set; } = null!;
        public bool IsPublic { get; set; }
        public bool Downloadable { get; set; }

        public string[] SongNames {get; set;} = null!;
        public string[] Genres {get; set;} = null!;
        public IFormFile[] SongFiles {get; set;} = null!;
    }
}