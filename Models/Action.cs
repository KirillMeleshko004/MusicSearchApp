using System.ComponentModel.DataAnnotations;

namespace MusicSearchApp.Models
{
    public class Action
    {
        public int ActionId { get; set; }
        public DateTime Date { get; set; }

        public int ActorId { get; set; }
        public User Actor { get; set; } = null!;
        
        public string Description { get; set; } = null!;
    }
}