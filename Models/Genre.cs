namespace MusicSearchApp.Models
{
    public class Genre
    {
        public string Name { get; set ; } = null!;
        // public string Description { get; set; } = null!;
        public int SongCount { get; set; }
        
        public ICollection<Song> Songs { get; set; } = null!;
    }
}