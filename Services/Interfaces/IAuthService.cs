using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool isSucceed, string message)> Registration(RegistrationViewModel model, string role);
        Task<(bool isSucceed, string token)> Login(AuthorizationViewModel model);
    }
}