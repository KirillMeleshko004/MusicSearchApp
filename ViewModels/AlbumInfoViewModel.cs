using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class AlbumInfoViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = null!;
        public bool IsPublic { get; set; }
        public bool Downloadable { get; set; }

        public ArtistViewModel Artist { get; set; } = null!;

        public int SongCount { get; set; }
        public string CoverImage { get; set; } = null!;


        public ICollection<SongInfoViewModel> Songs { get; set; } = null!;

        public AlbumInfoViewModel(Album album)
        {
            AlbumId = album.AlbumId;
            Title = album.Title;
            IsPublic = album.IsPublic;
            Downloadable = album.Downloadable;
            Artist = new ArtistViewModel(album.Artist);
            SongCount = album.SongCount;
            CoverImage = album.CoverImage;
        }
    }
}