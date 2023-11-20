using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Models.Static;
using MusicSearchApp.ViewModels;
using NAudio.Wave;

namespace MusicSearchApp.Services
{
    public class AlbumUploadingService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        private readonly FileService _fileService;
        private readonly RequestService _requestService;

        public AlbumUploadingService(UserManager<User> userManager, ApplicationContext context, 
            FileService fileService, RequestService requestService)
        {
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
            _requestService = requestService;
        }

        public async Task<bool> UploadAlbum(AlbumViewModel albumInfo)
        {
            bool isSucceed = true;
            string? coverImageName = await _fileService.SaveFile(albumInfo.CoverImage, FileService.FileType.AlbumImage);

            Album album = new()
            {
                ArtistId = albumInfo.ArtistId,
                Title = albumInfo.AlbumTitle,
                IsPublic = false,
                Downloadable = albumInfo.Downloadable,
                CoverImage = coverImageName!,
                SongCount = albumInfo.SongFiles.Length,
            };


            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();

            album = _context.Albums
                .Where(a => a.Title == albumInfo.AlbumTitle && a.ArtistId == albumInfo.ArtistId)
                .First();

            if(albumInfo.IsPublic) isSucceed = await _requestService.CreatePublishRequest(album);

            for(int i = 0; i < albumInfo.SongFiles.Length; i++)
            {
                isSucceed = await CreateSong(albumInfo.SongNames[i], album.AlbumId, album.ArtistId, 
                    albumInfo.SongFiles[i], albumInfo.Genres[i]);
            }

            return isSucceed;
        }
        private async Task<bool> CreateSong(string title, int albumId, int artistId, 
            IFormFile file, string genreName)
        {
            string? filePath = await _fileService.SaveFile(file, FileService.FileType.MusicFile);

            Genre? genre = _context.Genres.Where(g => g.Name == genreName).FirstOrDefault();
            if(genre == null)
            {
                genre = new Genre() { Name = genreName, SongCount = 1 };
                await _context.Genres.AddAsync(genre);
            }
            else
            {
                genre.SongCount += 1;
            }

            await _context.SaveChangesAsync();

            Song song = new()
            {
                Title = title,
                AlbumId = albumId,
                ArtistId = artistId,
                ReleaseDate = DateTime.Now,
                GenreName = genreName,
                FilePath = filePath!,
                Length = GetAudionLength(filePath!),
                ListenCount = 0
            };
            
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();

            return true;
        }

        private double GetAudionLength(string path)
        {
            Mp3FileReader reader = new Mp3FileReader(
                Path.Combine(Directory.GetCurrentDirectory(), "Data", path));

            return reader.TotalTime.TotalSeconds;
        }
    }
}