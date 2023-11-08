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

        public string GenerateToken(AuthorizationViewModel userData)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userData.UserName),
                new Claim(ClaimTypes.Role, userData.Role!)
            };
            
            //create jwt token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(EXPIRATION_TIME),
                signingCredentials: credentials);

            //return encoded jwt
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public void Logout()
        {
        }
    }
}