using MusicSearchApp.Models;

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
        public string CoverImage { get; set; } = null!;


        public SongInfoViewModel(Song song)
        {
            SongId = song.SongId;
            Title = song.Title;
            Album = new AlbumInfoViewModel(song.Album);
            Artist = new ProfileViewModel(song.Artist);
            Length = song.Length;
            ReleaseDate = song.ReleaseDate;
            GenreName = song.GenreName;
            ListenCount = song.ListenCount;
            FilePath = song.FilePath;
            CoverImage = song.Album.CoverImage;
        }
    }
}