namespace MusicSearchApp.Models
{
    public class RequestStatus
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        
        public ICollection<PublishRequest> Requests { get; set; } = null!;
    }
}