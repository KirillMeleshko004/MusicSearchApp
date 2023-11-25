namespace MusicSearchApp.Services.Interfaces
{
    public enum StatusCode
    {
        NotFound = 404,
        Created = 201,
        Ok = 200,
        InternalError = 500,
        Forbidden = 403
    }

    public interface IResponse<T>
    {
        StatusCode Status { get; set; }
        string Message { get; set; }
        T? Data { get; set; }
    }
}