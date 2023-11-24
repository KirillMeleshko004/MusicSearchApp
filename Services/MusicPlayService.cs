using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class MusicPlayService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        public MusicPlayService(ApplicationContext context, UserManager<User> userManager, FileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
        }

        public SongInfoViewModel? GetSong(int id)
        {
            return _context.Songs.Where(s => s.SongId == id)
                .Include(s => s.Artist)
                .Include(s => s.Album).ThenInclude(a => a.Artist)
                .Select<Song, SongInfoViewModel>(s => new(s)).FirstOrDefault();
        }

        const int pageCount = 10;
        public IEnumerable<SongInfoViewModel> GetSongs(int page, string? searchString = null)
        {
            return _context.Songs
                    .Where(s=> searchString.IsNullOrEmpty() || s.Title.Contains(searchString!))
                    .Include(s => s.Artist)
                    .Include(s => s.Album).ThenInclude(a => a.Artist)
                    .Where(s => s.Album.IsPublic)
                    .OrderByDescending(s => s.ListenCount)
                    .Skip(page * pageCount)
                    .Take(pageCount)
                    .Select<Song, SongInfoViewModel>(s => new(s));
        }

        public AlbumInfoViewModel? GetAlbum(int albumId)
        {
            Album? album = _context.Albums
                .Where(a => a.AlbumId == albumId)
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .FirstOrDefault();

            if(album == null) return null;

            AlbumInfoViewModel albumViewModel = new(album)
            {
                Songs = album.Songs.Select<Song, SongInfoViewModel>(s => new(s))
            };

            return albumViewModel;
        }

        public AlbumInfoViewModel? DeleteAlbum(int albumId)
        {
            Album? album = _context.Albums
                .Where(a => a.AlbumId == albumId)
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .FirstOrDefault();

            if(album == null) return null;

            AlbumInfoViewModel albumViewModel = new(album);

            foreach(var song in album.Songs)
            {
                _fileService.DeleteFile(song.FilePath);
            }
            _fileService.DeleteFile(album.CoverImage);
            
            _context.Albums.Remove(album);
            _context.SaveChanges();
            
            return albumViewModel;
        }

        public async Task<IEnumerable<AlbumInfoViewModel>?> GetLibrary(int userId)
        {
            if((await _userManager.FindByIdAsync(userId.ToString())) == null)
                return null;

            return _context.Albums
                .Where(a => a.ArtistId == userId)
                .Include(a => a.Artist)
                .Include(a => a.Request)
                .OrderBy(a => a.Request!.Date)
                .Select<Album, AlbumInfoViewModel>(a => new(a));
        }
    }
}