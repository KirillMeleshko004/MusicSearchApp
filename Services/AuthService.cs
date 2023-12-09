using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class AuthService : IAuthService
    {

        #region Dependecies

        //provide api for work with users in db. dbContext specified in builder.Services
        private readonly UserManager<User> userManager;

        //provide api for work with roles in db. dbContext specified in builder.Services
        private readonly RoleManager<IdentityRole<int>> roleManager;

        private readonly IAuthTokenGenerator _tokenGen;

        #endregion

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, 
            IAuthTokenGenerator authTokenGenerator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _tokenGen = authTokenGenerator;
        }

        public async Task<(bool isSucceed, string message)> Registration(RegistrationViewModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);

            if (userExists != null)
                return (false, "User already exists");

            User user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                DisplayedName = model.UserName,
                ProfileImage = "Images/Profile/default_profile_img.svg",
                Role = role,
                IsBlocked = false,
            };

            var createUserResult = await userManager.CreateAsync(user, model.Password);

            //if creation fails
            if (!createUserResult.Succeeded)
            {
                foreach(var error in createUserResult.Errors)
                {
                    System.Console.WriteLine(error.Description);
                }
                return (false ,"User creation failed! Please check user details and try again.");
            }

            //If role doesn't exist
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));

            //Assign role to user
            await userManager.AddToRoleAsync(user, role);

            return (true ,"User created successfully!");
        }

        public async Task<SessionDto?> Login(AuthorizationViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return null;
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return null;

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
              authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            string token = _tokenGen.GenerateToken(authClaims);

            SessionDto session = new()
            {
                UserId = user.Id,
                Username = user.UserName!,
                Role = user.Role!,
                Token = token,
                IsBlocked = user.IsBlocked
            };

            return session;
        }


        public async Task<IResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            
            IResponse<bool> response = 
                new Response<bool>();

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "User not found";
                return response;
            }

            IdentityResult res = await userManager
                .ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!res.Succeeded)
            {
                response.Status = StatusCode.Forbidden;
                response.Message = res.Errors
                    .Select(e => e.Description)
                    .Aggregate((d1, d2) => d1 + "\n" + d2);

                response.Data = false;
            }
            else
            {
                response.Status = StatusCode.Ok;
                response.Message = "Success";
                response.Data = true;

            }

            return response;
        }
    }
}