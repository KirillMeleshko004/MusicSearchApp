using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Models.Static;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class MusicControlService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        private readonly ActionService _actionService;
        public MusicControlService(ApplicationContext context, UserManager<User> userManager,
            FileService fileService, ActionService actionService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
            _actionService = actionService;
        }
        public async Task<IResponse<AlbumInfoViewModel>> DeleteAlbum(int albumId, string actorName)
        {
            IResponse<AlbumInfoViewModel> response = new Response<AlbumInfoViewModel>();
      
            User? actor = await _userManager.FindByNameAsync(actorName);

            Album? album = _context.Albums
                .Where(a => a.AlbumId == albumId)
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .FirstOrDefault();

            if(album == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Album not found";
                return response;
            }

            if(actor == null || 
                (actor.UserName != album.Artist.UserName && actor.Role != UserRoles.Admin))
            {
                response.Status = StatusCode.Forbidden;
                response.Message = "Invalid actor";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(album);

            foreach(var song in album.Songs)
            {
                _fileService.DeleteFile(song.FilePath);
            }
            _fileService.DeleteFile(album.CoverImage);
            
            _context.Albums.Remove(album);
            _context.SaveChanges();
            
            await _actionService.CreateAction(album.ArtistId, 
                "Deleted album " + album.Title);
            
            return response;
        }
    }
}