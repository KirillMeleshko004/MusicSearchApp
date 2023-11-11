using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MusicSearchApp.Models.Static;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly IAuthService _authService;


        public AccountController(ApplicationContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        [HttpPost]
        [Route("{action}")] 
        public async Task<IActionResult> Register(RegistrationViewModel userData)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { errorMessage = "Invalid payload" });

                var (isSucceed, message) = await _authService.Registration(userData, UserRoles.User);

                if (!isSucceed)
                {
                    return BadRequest(new { errorMessage = message });
                }

                return CreatedAtAction(nameof(Register), userData);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { errorMessage = ex.Message});
            }
        }

        [HttpPost]  
        [Route("{action}")]  
        public async Task<IActionResult> Login([FromBody]AuthorizationViewModel userData)  
        {  
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { errorMessage = "Invalid payload" });

                //token should be renamed
                var (isSucceed, token) = await _authService.Login(userData);
                if (!isSucceed)
                    return BadRequest(new { errorMessage = token});

                return Ok(new { token });
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { errorMessage = ex.Message});
            }
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin, User")]
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
            
            // System.Console.WriteLine(ControllerContext.HttpContext.User.Identity!.AuthenticationType);
            // System.Console.WriteLine();
            // foreach(var claim in ControllerContext.HttpContext.User.Claims) System.Console.WriteLine(claim.Value);
            // System.Console.WriteLine();
            return Ok( new { info = "Authorized" });
        }
    }
}