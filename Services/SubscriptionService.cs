using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSearchApp.Models;
using MusicSearchApp.Models.DB;
using MusicSearchApp.Services.Interfaces;
using MusicSearchApp.ViewModels;

namespace MusicSearchApp.Services
{
    public class SubscriptionService
    {
        
        private readonly ApplicationContext _context;
        public SubscriptionService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IResponse<SubsciptionViewModel>> SubscribeAsync(int userId, int artistId)
        {
            IResponse<SubsciptionViewModel> response = 
                new Response<SubsciptionViewModel>();

            if(await _context.Subscriptions
                .AnyAsync(s => s.SubscriberId == userId && s.ArtistId == artistId))
            {
                response.Status = StatusCode.Forbidden;
                response.Message = "You already subscribed to artist";
                return response;
            }

            if(!await _context.Users.AnyAsync(u => u.Id == userId) ||
                !await _context.Users.AnyAsync(u => u.Id == artistId))
            {
                response.Status = StatusCode.NotFound;
                response.Message = "User not found";
                return response;
            }

            Subscription subscription = new()
            {
                ArtistId = artistId,
                SubscriberId = userId,
                StartDate = DateTime.Now
            };

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            subscription = (await _context.Subscriptions.AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Subscriber)
                .FirstOrDefaultAsync(s => s.SubscriberId == userId && s.ArtistId == artistId))!;

            response.Status = StatusCode.Created;
            response.Message = "Success";
            response.Data = new(subscription);

            return response;
        }

        public async Task<IResponse<SubsciptionViewModel>> UnsubscribeAsync(int userId, int artistId)
        {
            IResponse<SubsciptionViewModel> response = 
                new Response<SubsciptionViewModel>();

            Subscription? subscription = await _context.Subscriptions.AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Subscriber)
                .FirstOrDefaultAsync(s => s.SubscriberId == userId && s.ArtistId == artistId);

            if(subscription == null)
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Subscription not found";
                return response;
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            
            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = new(subscription);

            return response;
        }
    
        public IResponse<IEnumerable<SubsciptionViewModel>> GetSubscriptions(int userId)
        {
            IResponse<IEnumerable<SubsciptionViewModel>> response = 
                new Response<IEnumerable<SubsciptionViewModel>>();

            IEnumerable<SubsciptionViewModel> subscriptions = 
                _context.Subscriptions.AsNoTracking()
                    .Where(s => s.SubscriberId == userId)
                    .Include(s => s.Artist)
                    .Select<Subscription, SubsciptionViewModel>(s => new(s));

            if(subscriptions.IsNullOrEmpty())
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Subscriptions not found";
                return response;
            }

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = subscriptions;

            return response;
        }

        public async Task<IResponse<bool>> IsSubscribedAsync(int subscriberId, int artistId)
        {
            IResponse<bool> response = 
                new Response<bool>();

            Subscription? subscription = await _context.Subscriptions.AsNoTracking()
                .Include(s => s.Artist)
                .Include(s => s.Subscriber)
                .FirstOrDefaultAsync(s => s.SubscriberId == subscriberId && s.ArtistId == artistId);

            response.Status = StatusCode.Ok;
            response.Message = "Success";
            response.Data = subscription != null;

            return response;
        }
    }
}