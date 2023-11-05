namespace MusicSearchApp.Models
{
    public class Action
    {
        public DateTime Date { get; set; }
        public int ActorId { get; set; }
        public string Description { get; set; } = null!;
    }
}