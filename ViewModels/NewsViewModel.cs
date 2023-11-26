using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }
        public DateTime Date { get; set; }
        public AlbumInfoViewModel Album { get; set; } = null!;


        public NewsViewModel(News news)
        {
            NewsId = news.NewsId;
            Date = news.Date;
            Album = new(news.Album);
        }
    }
}