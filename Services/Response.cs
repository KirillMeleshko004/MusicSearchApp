using MusicSearchApp.Services.Interfaces;

namespace MusicSearchApp.Services
{
    public class Response<T> : IResponse<T>
    {
        public StatusCode Status { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}