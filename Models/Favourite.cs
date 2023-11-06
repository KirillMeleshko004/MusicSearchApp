using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSearchApp.Models
{
    public class Favourite
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int SongId { get; set; }
        public Song Song { get; set; } = null!;
        
        public DateTime AdditionDate { get; set; }
    }
}