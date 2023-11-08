using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IAuthTokenGenerator _tokenGen;

        public AccountController(ApplicationContext context, IAuthTokenGenerator tokenGen)
        {
            _context = context;
            _tokenGen = tokenGen;
        }


        [HttpPost]
        [Route("{action}")] 
        public object Register(RegistrationViewModel userData)
        {
            if(!ModelState.IsValid) return new { info = "Invalid Data" };
            return userData;
        }

        private IEnumerable<User> _db = new List<User>()
        {
            new() { UserId = 1, UserName = "FirstUser", Password = "qwerty", Role = "User" },
            new() { UserId = 2, UserName = "SecondUser", Password = "asdfg", Role = "User" },
            new() { UserId = 3, UserName = "Admin", Password = "admin", Role = "Admin" },
        };

        [HttpPost]  
        [Route("{action}")]  
        public IActionResult Login(AuthorizationViewModel userData)  
        {  
            System.Console.WriteLine(ControllerContext.HttpContext.User.Identity?.IsAuthenticated);
            System.Console.WriteLine(ControllerContext.HttpContext.User.Identity?.AuthenticationType);
            if(!ModelState.IsValid) 
                return BadRequest(new { status = 400, isSuccess = false, message = "Invalid user data"});

            // AuthorizationViewModel? user = _context.Users
            //     .Where(u => u.UserName == userData.UserName)
            //     .Select(u => new AuthorizationViewModel() 
            //     { 
            //         UserName = u.UserName,
            //         Password = u.Password,
            //         Role = u.Role
            //     }).FirstOrDefault();
            AuthorizationViewModel? user = _db
                .Where(u => u.UserName == userData.UserName)
                .Select(u => new AuthorizationViewModel() 
                { 
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role
                }).FirstOrDefault();

            if(user == null)
                return Unauthorized(new { status = 401, isSuccess = false, message = "User not found"});

            if(user.Password != userData.Password)
                return Unauthorized(new { status = 401, isSuccess = false, message = "Incorrect password"});

            var token = _tokenGen.GenerateToken(user);
                
            return Ok(new { token, user});
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Logout(AuthorizationViewModel user)
        {
            ControllerContext.HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Route("{action}")]
        public IActionResult Test()
        {
            return Ok( new { info = "Authorized" });
        }
    }
}