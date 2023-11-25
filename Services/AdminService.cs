using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class AdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public AdminService(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _context = applicationContext;
        }

        public async Task<IResponse<ProfileViewModel>> ChangeBlockAsync(int id, string actorName)
        {
            IResponse<ProfileViewModel> response = 
                new Response<ProfileViewModel>();

            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(actorName == user!.UserName)
            {   
                response.Status = StatusCode.Forbidden;
                response.Message = "Can not change status of yourself";
                return response;
            }

            if(user == null)
            {   
                response.Status = StatusCode.NotFound;
                response.Message = "No users matching conditions";
                return response;
            }

            user.IsBlocked = !user.IsBlocked;
            await _userManager.UpdateAsync(user);

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(user);

            return response;
        }


        public async Task<ProfileViewModel?> DeleteUserAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null) return null;

            await _userManager.DeleteAsync(user);
            return new ProfileViewModel(user);
        }

        public IResponse<IEnumerable<ProfileViewModel>> GetUsers(string? searchString = null)
        {
            IResponse<IEnumerable<ProfileViewModel>> response = 
                new Response<IEnumerable<ProfileViewModel>>();

            IEnumerable<ProfileViewModel> users = _userManager.Users
                .Where(u => searchString.IsNullOrEmpty() 
                    || u.UserName!.ToLower().Contains(searchString!.ToLower()))
                .Select(u => new ProfileViewModel(u));

            if(users.IsNullOrEmpty())
            {
                response.Status = StatusCode.NotFound;
                response.Message = "No users matching conditions";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = users;

            return response;
        }
    
        public async Task<IResponse<ProfileViewModel>> GetByIdAsync(int id)
        {
            User? user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null) return new Response<ProfileViewModel>() 
                { Status = StatusCode.NotFound, Message = "User not found"};

            
            return new Response<ProfileViewModel>() 
                { Status = StatusCode.Ok, Message = "Success", Data = new(user) };
        }
    }
}