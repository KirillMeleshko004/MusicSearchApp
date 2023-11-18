using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;

namespace MusicSearchApp.Services
{
    public class MusicPlayService
    {
        private readonly ApplicationContext _context;
        public MusicPlayService(ApplicationContext context)
        {
            _context = context;
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
    }
}