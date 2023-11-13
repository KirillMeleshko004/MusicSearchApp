using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Services.Interfaces;

namespace MusicSearchApp.Services
{
    public class JWTTokenGenerator : IAuthTokenGenerator
    {

        private IConfiguration _config;
        
        public JWTTokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["jwt:Issuer"],
                Audience = _config["jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(Double.Parse(_config["jwt:Expiration"]!)),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}