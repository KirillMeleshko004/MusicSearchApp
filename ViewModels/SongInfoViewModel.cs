using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class SongInfoViewModel
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;

        public AlbumInfoViewModel Album { get; set; } = null!;
        public ArtistViewModel Artist { get; set; } = null!;

        public double Length { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string? GenreName { get; set; }

        public int ListenCount { get; set; }
        public string FilePath { get; set; } = null!;
        public string? CoverImage { get; set; } = null!;
        public bool Downloadable { get; set; }


        public SongInfoViewModel(Song song, bool downloadable = false)
        {
            SongId = song.SongId;
            Title = song.Title;

            if(song.Album != null)
                Album = new AlbumInfoViewModel(song.Album);
            if(song.Artist != null)
                Artist = new ArtistViewModel(song.Artist);

            Length = song.Length;
            ReleaseDate = song.ReleaseDate;
            GenreName = song.GenreName;
            ListenCount = song.ListenCount;
            FilePath = song.FilePath;
            CoverImage = song.Album?.CoverImage;
            Downloadable = downloadable || song.Album?.Downloadable == true;
        }
    }
}