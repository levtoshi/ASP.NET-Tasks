using HomeWork_09._02._2026.Interfaces;
using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;
using HomeWork_09._02._2026.Storage;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_09._02._2026.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers(UserRequest request)
        {
            var users = _context.Users.Include(u => u.Subscriptions).AsNoTracking();

            if (request.NameStartsWith != null)
            {
                users = users.Where(u => u.Name.StartsWith(request.NameStartsWith.Value));
            }
            if (request.HasSubscription != null)
            {
                if (request.HasSubscription.Value)
                {
                    users = users.Where(u => u.Subscriptions.Count != 0);
                }
                else
                {
                    users = users.Where(u => u.Subscriptions.Count == 0);
                }
            }

            return await users.ToListAsync();
        }

        public async Task<List<string>> GetPremiumUsersNames()
        {
            return await _context.Users
                .Include(u => u.Subscriptions)
                .AsNoTracking()
                .Where(u => u.Subscriptions.Any(s => s.Type == SubscriptionType.Premium))
                .Select(u => u.Name)
                .Take(5)
                .ToListAsync();
        }

        public async Task<User> GetUserThatHasMostSubs()
        {
            return await _context.Users
                .Include(u => u.Subscriptions)
                .AsNoTracking()
                .OrderByDescending(u => u.Subscriptions.Count)
                .FirstAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}