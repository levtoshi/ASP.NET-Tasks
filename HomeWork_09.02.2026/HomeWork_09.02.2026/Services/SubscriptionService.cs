using HomeWork_09._02._2026.Interfaces;
using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;
using HomeWork_09._02._2026.Responses;
using HomeWork_09._02._2026.Storage;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_09._02._2026.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataContext _context;

        public SubscriptionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetFreeExpiredSubscriptions()
        {
            return await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.Type == SubscriptionType.Free && s.EndDate < DateOnly.FromDateTime(DateTime.UtcNow))
                .ToListAsync();
        }

        public async Task<List<SubscriptionTypeCountDto>> GetGroupedBy(SubscriptionRequest request)
        {
            switch (request.GroupedBy)
            {
                case "subs":
                    return await _context.Subscriptions
                        .AsNoTracking()
                        .GroupBy(s => s.Type)
                        .Select(g => new SubscriptionTypeCountDto
                        {
                            Type = g.Key,
                            Count = g.Count()
                        })
                        .ToListAsync();
                case "users":
                    return await _context.Users
                        .AsNoTracking()
                        .SelectMany(u => u.Subscriptions)
                        .GroupBy(s => s.Type)
                        .Select(g => new SubscriptionTypeCountDto
                        {
                            Type = g.Key,
                            Count = g
                                .Select(s => s.UserId)
                                .Distinct()
                                .Count()
                        })
                        .ToListAsync();
                default:
                    throw new Exception("Incorrect groupedBy query!");
            }
        }

        public async Task<List<Subscription>> GetExpensiveSubs()
        {
            decimal avgPrice = _context.Subscriptions.Select(s => s.Price).Average();

            return await _context.Subscriptions
                .AsNoTracking()
                .Where(s => s.Price > avgPrice)
                .ToListAsync();
        }

        public async Task<Subscription> CreateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }
    }
}