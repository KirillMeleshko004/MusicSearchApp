using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;

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

        [HttpPost]  
        [Route("{action}")]  
        public IActionResult Login(AuthorizationViewModel userData)  
        {  
            if(!ModelState.IsValid) 
                return Unauthorized(new { status = 401, isSuccess = false, message = "Invalid data"});
            
            userData.Role = "Admin";

            var token = _tokenGen.GenerateToken(userData);
                
            return Ok(new {token = token, user = userData});
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