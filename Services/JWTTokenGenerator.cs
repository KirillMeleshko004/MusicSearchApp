using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class JWTTokenGenerator : IAuthTokenGenerator
    {
        private const int EXPIRATION_TIME = 120;

        private IConfiguration _config;
        
        public JWTTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:ValidIssuer"],
                Audience = _config["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddMinutes(EXPIRATION_TIME),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}