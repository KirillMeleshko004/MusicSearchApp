using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;

namespace MusicSearchApp.Controllers
{

    
    [Route("api/{controller}")]
    public class SongController : Controller
    {
        private readonly ApplicationContext _context;

        public SongController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult Play(int SongId)
        {
            Song? song = _context.Songs.Where(s => s.SongId == SongId).FirstOrDefault();

            if(song == null) return NotFound();

            string path = "../Music/" + song.FilePath;

            byte[] bytes = Array.Empty<byte>();

            using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(path).Length;
                bytes = br.ReadBytes((int)numBytes);
            }

            return File(bytes, "audio/mpeg");
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult Get(int first, int last)
        {
            IEnumerable<Song> songs = _context.Songs
                .SkipWhile(s => s.SongId != first)
                .TakeWhile(s => s.SongId <= last);

            return Json(songs);
        }
    }
}