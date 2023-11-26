using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class NewsService
    {
        private readonly ApplicationContext _context;
        public NewsService(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<bool> CreatePublishNews(Album album)
        {
            News news = new()
            {
                Date = DateTime.Now,
                AlbumId = album.AlbumId,
            };

            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            return true;
        }


        public IResponse<IEnumerable<NewsViewModel>> GetNews(int start, int end)
        {
            IResponse<IEnumerable<NewsViewModel>> response = 
                new Response<IEnumerable<NewsViewModel>>();

            IEnumerable<NewsViewModel> news = _context.News
                .Include(n => n.Album).ThenInclude(n => n.Artist)
                .OrderByDescending(n => n.Date)
                .Skip(start)
                .Take(end)
                .Select<News, NewsViewModel>(n => new(n));

            
            if(news == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Request not found";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = news;

            return response;
        }
    }
}