using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class SongInfoViewModel
    {
        public int SongId { get; set; }
        public string Title { get; set; } = null!;

        public AlbumInfoViewModel? Album { get; set; } = null!;
        public ArtistViewModel? Artist { get; set; } = null!;

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

            Album = song.Album != null ? new(song.Album) : null;
            Artist = song.Album != null ? new(song.Artist) : null;

            Length = song.Length;
            ReleaseDate = song.ReleaseDate;
            GenreName = song.GenreName;
            ListenCount = song.ListenCount;
            FilePath = song.FilePath;
            CoverImage = Album?.CoverImage;
            Downloadable = downloadable || song.Album?.Downloadable == true;
        }

        public SongInfoViewModel(Song song, AlbumInfoViewModel? albumInfoViewModel,
             ArtistViewModel? artistViewModel, bool downloadable = false)
        {
            SongId = song.SongId;
            Title = song.Title;

            Album = albumInfoViewModel ?? 
                (song.Album != null ? new(song.Album) : null);
            Artist = artistViewModel ?? 
                (song.Album != null ? new(song.Artist) : null);

            Length = song.Length;
            ReleaseDate = song.ReleaseDate;
            GenreName = song.GenreName;
            ListenCount = song.ListenCount;
            FilePath = song.FilePath;
            CoverImage = Album?.CoverImage;
            Downloadable = downloadable || song.Album?.Downloadable == true;
        }
    }
}