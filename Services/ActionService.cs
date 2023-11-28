using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class ActionService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public ActionService(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAction(int actorId, string description)
        {
            try
            {
                MusicSearchApp.Models.Action action = new()
                {
                    ActorId = actorId,
                    Date = DateTime.Now,
                    Description = description
                };

                await _context.Actions.AddAsync(action);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> CreateAction(string actorName, string description)
        {
            try
            {
                User actor = (await _userManager.FindByNameAsync(actorName))!;

                MusicSearchApp.Models.Action action = new()
                {
                    ActorId = actor.Id,
                    Date = DateTime.Now,
                    Description = description
                };

                await _context.Actions.AddAsync(action);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;
            }
        }

        public IResponse<IEnumerable<ActionViewModel>> GetActionsForUser(int actorId)
        {
            IResponse<IEnumerable<ActionViewModel>> response = 
                new Response<IEnumerable<ActionViewModel>>();
            try
            {
                IEnumerable<ActionViewModel> actions =
                    _context.Actions.Where(a => a.ActorId == actorId)
                    .Select<Models.Action, ActionViewModel>(a => new(a));

                if(actions.IsNullOrEmpty())
                {
                    response.Status = StatusCode.NotFound;
                    response.Message = "Actions not found";
                    return response;
                } 

                
                response.Status = StatusCode.Ok;
                response.Message = "Success";
                response.Data = actions;

                return response;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                response.Status = StatusCode.InternalError;
                response.Message = "Server internal error";
                return response;
            }
        }

        public IResponse<IEnumerable<ActionViewModel>> GetActions(int first, int count) 
        {
            IResponse<IEnumerable<ActionViewModel>> response = 
                new Response<IEnumerable<ActionViewModel>>();

            try
            {
                IEnumerable<ActionViewModel> actions =
                    _context.Actions.Include(a => a.Actor)
                    .OrderByDescending(a => a.Date)
                    .Skip(first)
                    .Take(count)
                    .Select<Models.Action, ActionViewModel>(a => new(a));

                if(actions.IsNullOrEmpty())
                {
                    response.Status = StatusCode.NotFound;
                    response.Message = "Actions not found";
                    return response;
                } 

                response.Status = StatusCode.Ok;
                response.Message = "Success";
                response.Data = actions;

                return response;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                response.Status = StatusCode.InternalError;
                response.Message = "Server internal error";
                return response;
            }
        }
    }
}