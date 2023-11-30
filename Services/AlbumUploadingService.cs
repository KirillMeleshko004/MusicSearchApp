using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Models.Static;
using MusicSearchApp.Services.Interfaces;
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
        private readonly ActionService _actionService;

        public AlbumUploadingService(UserManager<User> userManager, ApplicationContext context, 
            FileService fileService, RequestService requestService, ActionService actionService)
        {
            _userManager = userManager;
            _context = context;
            _fileService = fileService;
            _requestService = requestService;
            _actionService = actionService;
        }

        public async Task<IResponse<AlbumInfoViewModel>> UploadAlbum(AlbumViewModel albumInfo)
        {
            IResponse<AlbumInfoViewModel> response = new Response<AlbumInfoViewModel>();

            bool isSucceed = true;

            if(await _context.Albums.AnyAsync(a => a.Title == albumInfo.AlbumTitle && a.ArtistId == albumInfo.ArtistId))
            {
                response.Status = StatusCode.BadRequest;
                response.Message = "You already have an album with the same name!";
                return response;
            }

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

            for(int i = 0; i < albumInfo.SongFiles.Length; i++)
            {
                isSucceed = await CreateSong(albumInfo.SongNames[i], album.AlbumId, album.ArtistId, 
                    albumInfo.SongFiles[i], albumInfo.Genres[i]);
            }
            
            if(!isSucceed)
            {
                response.Status = StatusCode.InternalError;
                response.Message = "Internal server error while creating album";
                return response;
            }

            await _actionService.CreateAction(albumInfo.ArtistId, 
                "Added album " + albumInfo.AlbumTitle);

            if(albumInfo.IsPublic) 
            {
                isSucceed = await _requestService.CreatePublishRequest(album);
                await _actionService.CreateAction(albumInfo.ArtistId, 
                    "Created publish request for album " + albumInfo.AlbumTitle);
            }    

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(album);

            await _context.SaveChangesAsync();
            return response;
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
                await _context.SaveChangesAsync();
            }
            else
            {
                genre.SongCount += 1;
            }

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

        private static double GetAudionLength(string path)
        {
            Mp3FileReader reader = new Mp3FileReader(
                Path.Combine(Directory.GetCurrentDirectory(), "Data", path));

            return reader.TotalTime.TotalSeconds;
        }
    }
}