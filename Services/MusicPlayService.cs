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
        public MusicPlayService(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Song> GetSong(int id)
        {
            return (await _context.Songs.FindAsync(id))!;
        }

        public async Task<byte[]?> Play(int id)
        {
            Song? song = await GetSong(id);

            if(song == null) return null;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", song.FilePath);

            byte[] bytes = Array.Empty<byte>();

            using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(path).Length;
                bytes = br.ReadBytes((int)numBytes);
            }

            return bytes;
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