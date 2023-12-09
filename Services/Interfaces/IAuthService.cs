using System.Security.Claims;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool isSucceed, string message)> Registration(RegistrationViewModel model, string role);
        Task<SessionDto?> Login(AuthorizationViewModel model);
        public Task<IResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}