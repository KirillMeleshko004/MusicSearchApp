using Microsoft.EntityFrameworkCore;
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

            if(!await _context.Subscriptions
                .AnyAsync(s => s.SubscriberId == userId && s.ArtistId == artistId))
            {
                response.Status = StatusCode.NotFound;
                response.Message = "Subscription not found";
                return response;
            }

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
    }
}