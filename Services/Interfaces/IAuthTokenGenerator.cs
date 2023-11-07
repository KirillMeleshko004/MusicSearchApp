using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services.Interfaces
{
    public interface IAuthTokenGenerator
    {
        public string GenerateToken(AuthorizationViewModel userData);
    }
}