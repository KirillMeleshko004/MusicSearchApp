using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Models.Static;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class RequestService
    {
        private readonly ApplicationContext _context;
        private readonly NewsService _newsService;
        private readonly ActionService _actionService;
        public RequestService(ApplicationContext context, NewsService newsService,
            ActionService actionService)
        {
            _context = context;
            _newsService = newsService;
            _actionService = actionService;
        }


        private async Task<RequestStatus> GetStatus(string name)
        {
            RequestStatus? status = _context.Statuses
                .Where(s => s.Status == name)
                .FirstOrDefault();

            if(status == null)
            {
                RequestStatus requestStatus = new() {Status = name};
                await _context.Statuses.AddAsync(requestStatus);
                await _context.SaveChangesAsync();
                status = _context.Statuses
                    .Where(s => s.Status == name)
                    .FirstOrDefault();
            }

            return status!;
        }

        public async Task<bool> CreatePublishRequest(Album album)
        {
            RequestStatus pending = await GetStatus(RequestStatuses.Pending);

            PublishRequest request = new()
            {
                Date = DateTime.Now,
                ArtistId = album.ArtistId,
                AlbumId = album.AlbumId,
                StatusId = pending.Id,
            };

            await _context.PublishRequests.AddAsync(request);
            await _context.SaveChangesAsync();

            return true;
        }
        
        private async Task<RequestViewModel?> AcceptRequestAsync(int requestId)
        {
            PublishRequest? request = _context.PublishRequests
                .Where(r => r.RequestId == requestId)
                .Include(r => r.Album)
                .Include(r => r.Artist)
                .FirstOrDefault();

            if(request == null) return null;

            if(!await _newsService.CreatePublishNews(request.Album)) return null;

            RequestStatus accepted = await GetStatus(RequestStatuses.Accepted);

            request.StatusId = accepted.Id;
            request.Album.IsPublic = true;

            await _context.SaveChangesAsync();

            return new(request);
        }

        private async Task<RequestViewModel?> DenyRequestAsync(int requestId)
        {
            PublishRequest? request = _context.PublishRequests
                .Where(r => r.RequestId == requestId)
                .Include(r => r.Album)
                .Include(r => r.Artist)
                .FirstOrDefault();
                
            if(request == null) return null;

            RequestStatus deny = await GetStatus(RequestStatuses.Denied);

            request.StatusId = deny.Id;

            await _context.SaveChangesAsync();

            return new(request);
        }
    
        public async Task<IResponse<RequestViewModel>> ChangeStatusAsync(int requestId, string status,
            string actorName)
        {
            RequestViewModel? request = null;
            if(status == RequestStatuses.Accepted) request = await AcceptRequestAsync(requestId);
            else if (status == RequestStatuses.Denied) request = await DenyRequestAsync(requestId);

            IResponse<RequestViewModel> response = 
                new Response<RequestViewModel>();

            if(request == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Request not found";
                return response;
            }
    
            await _actionService.CreateAction(actorName, 
                "Changed status of album " + request?.Album.Title + " to " + status);

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = request;

            return response;
        }

        public async Task<IResponse<IEnumerable<RequestViewModel>>> GetPendingRequestsAsync()
        {
            IResponse<IEnumerable<RequestViewModel>> response = 
                new Response<IEnumerable<RequestViewModel>>();

            RequestStatus pending = await GetStatus(RequestStatuses.Pending);

            IEnumerable<RequestViewModel> requests = _context.PublishRequests
                .Where(r => r.StatusId == pending.Id)
                .Include(r => r.Artist)
                .Include(r => r.Album)
                .Select<PublishRequest, RequestViewModel>(r => new(r));
            
            if(requests.IsNullOrEmpty())
            {
                response.Status = StatusCode.NotFound;
                response.Message = "No pending requests";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = requests;

            return response;
        }
    }
}