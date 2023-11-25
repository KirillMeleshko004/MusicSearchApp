using MusicSearchApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicSearchApp.Models.DB;
using Microsoft.AspNetCore.Identity;
using MusicSearchApp.Models;
using Microsoft.AspNetCore.Authorization;
using MusicSearchApp.Services;
using MusicSearchApp.Services.Interfaces;

namespace MusicSearchApp.Controllers
{
    [Route("api/{controller}")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly AdminService _adminService;
        private readonly RequestService _requestService;

        public AdminController(ApplicationContext context, UserManager<User> userManager,
            AdminService adminService, RequestService requestService)
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
            _requestService = requestService;
        }

        #region User Management

        [HttpGet]
        [Route("users/{action}")]
        public IActionResult Find([FromQuery]string username)
        {
            
            IResponse<IEnumerable<ProfileViewModel>> result = _adminService.GetUsers(username);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new { users = result.Data, message = result.Message });
        }

        [HttpGet]
        [Route("users/{action}/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            IResponse<ProfileViewModel> result = await _adminService.GetByIdAsync(id);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { user = result.Data, message = result.Message });
        }

        //Change user status (active/blocked)
        [HttpPatch]
        [Route("users/{action}/{id}")]
        public async Task<IActionResult> ChangeBlock(int id)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            IResponse<ProfileViewModel> result = await _adminService.ChangeBlockAsync(id, actorName);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { profile = result.Data, message = result.Message });
        }
    
        [HttpDelete]
        [Route("users/{action}/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string actorName = ControllerContext.HttpContext.User.Identity!.Name!;

            IResponse<ProfileViewModel> result = await _adminService.DeleteUserAsync(id, actorName);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
                return StatusCode((int)result.Status, new { errorMessage = result.Message });

            return Ok(new { user = result.Data, message = result.Message });
        }
    
        #endregion

        #region Requests Management

        [HttpGet]
        [Route("requests/{action}")]
        public async Task<IActionResult> GetPending()
        {
            IResponse<IEnumerable<RequestViewModel>> result = 
                await _requestService.GetPendingRequests();

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new{requests = result.Data, message = result.Message });
        }

        [HttpPatch]
        [Route("requests/{action}/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            if(!ModelState.IsValid) return BadRequest();

            IResponse<RequestViewModel> result = 
                await _requestService.ChangeStatus(id, status);

            if(result.Status != Services.Interfaces.StatusCode.Ok)
            {
                return StatusCode((int)result.Status, new { errorMessage = result.Message });
            }

            return Ok(new{request = result.Data, message = result.Message });
        }
        
        #endregion
    }
}