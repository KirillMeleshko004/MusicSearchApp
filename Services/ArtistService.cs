using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class ArtistService
    {
        
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public ArtistService(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IResponse<ArtistViewModel> GetArtist(int id)
        {
            IResponse<ArtistViewModel> response = new Response<ArtistViewModel>();

            ArtistViewModel? artist = _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Albums)
                .Select<User, ArtistViewModel>(u => 
                    new(u) { Albums = u.Albums.Where(a => a.IsPublic)
                        .Select<Album, AlbumInfoViewModel>(a => new(a))})
                .FirstOrDefault();

            if(artist == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Artist not found";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = artist;

            return response;
        }
    }
}