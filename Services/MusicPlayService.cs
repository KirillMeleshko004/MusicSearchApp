using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class MusicPlayService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        public MusicPlayService(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IResponse<SongInfoViewModel> GetSong(int id)
        {
            IResponse<SongInfoViewModel> response = new Response<SongInfoViewModel>();

            Song? song = _context.Songs.Where(s => s.SongId == id)
                .Include(s => s.Artist)
                .Include(s => s.Album).ThenInclude(a => a.Artist).FirstOrDefault();

            if(song == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Song not found";

                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(song);

            return response;
        }

        public IResponse<IEnumerable<SongInfoViewModel>> GetSongs(string? searchString = null)
        {
            IResponse<IEnumerable<SongInfoViewModel>> response = 
                new Response<IEnumerable<SongInfoViewModel>>();

            Song? song = _context.Songs
                .Where(s=> searchString.IsNullOrEmpty() || s.Title.Contains(searchString!))
                .Include(s => s.Album)
                .Where(s => s.Album.IsPublic)
                .Include(s => s.Artist)
                .FirstOrDefault();
                
            if(song == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "No songs matching conditions";
                return response;
            }

            IEnumerable<SongInfoViewModel> songs = _context.Songs
                    .Where(s=> searchString.IsNullOrEmpty() || s.Title.Contains(searchString!))
                    .Where(s => s.Album.IsPublic)
                    .Include(s => s.Artist)
                    .Include(s => s.Album).ThenInclude(a => a.Artist)
                    .OrderByDescending(s => s.ListenCount)
                    .Select<Song, SongInfoViewModel>(s => 
                        new(s, s.Album.Downloadable));

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = songs;

            return response;
        }

        public IResponse<AlbumInfoViewModel> GetAlbum(int albumId)
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
            AlbumInfoViewModel albumInfo = new(album);
            albumInfo.Songs = album.Songs.Select<Song, SongInfoViewModel>(s => 
                    new(s, albumInfo, albumInfo.Artist, album.Downloadable));

            response.Data = albumInfo;

            return response;
        }

        public async Task<IResponse<IEnumerable<AlbumInfoViewModel>>> GetLibrary(int userId)
        {
            IResponse<IEnumerable<AlbumInfoViewModel>> response = 
                new Response<IEnumerable<AlbumInfoViewModel>>();

            if((await _userManager.FindByIdAsync(userId.ToString())) == null)
            {
                response.Status = StatusCode.Forbidden;
                response.Message = "Forbidden";
                return response;
            }
            IEnumerable<AlbumInfoViewModel> albums = _context.Albums
                .Where(a => a.ArtistId == userId)
                .Include(a => a.Artist)
                .Include(a => a.Request).ThenInclude(r => r!.Status)
                .OrderBy(a => a.Request!.Date)
                .Select<Album, AlbumInfoViewModel>(a => new(a));

            if(albums.IsNullOrEmpty())
            {
                response.Status = StatusCode.NotFound;
                response.Message = "No albums in library";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = albums;

            return response;
        }
    }
}