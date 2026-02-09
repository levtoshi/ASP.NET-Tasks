using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;

namespace HomeWork_09._02._2026.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(UserRequest request);
        Task<List<string>> GetPremiumUsersNames();
        Task<User> GetUserThatHasMostSubs();
        Task<User> CreateUser(User user);
    }
}