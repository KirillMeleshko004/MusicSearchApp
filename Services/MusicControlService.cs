using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class MusicControlService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        public MusicControlService(ApplicationContext context, UserManager<User> userManager, FileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
        }
        public IResponse<AlbumInfoViewModel> DeleteAlbum(int albumId)
        {
            IResponse<AlbumInfoViewModel> response = new Response<AlbumInfoViewModel>();

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
            
            return response;
        }
    }
}