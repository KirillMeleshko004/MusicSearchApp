using System.Security.Claims;

namespace MusicSearchApp.Services.Interfaces
{
    public interface IAuthTokenGenerator
    {
        public string GenerateToken(IEnumerable<Claim> claims);
    }
}