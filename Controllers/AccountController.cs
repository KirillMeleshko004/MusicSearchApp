using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
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
                
            return Ok(new { status = 200, isSuccess = true, message = "Not implemented"} );
        }  
    }
}