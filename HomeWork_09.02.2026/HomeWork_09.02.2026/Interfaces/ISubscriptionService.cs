using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;
using HomeWork_09._02._2026.Responses;

namespace HomeWork_09._02._2026.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetFreeExpiredSubscriptions();
        Task<List<SubscriptionTypeCountDto>> GetGroupedBy(SubscriptionRequest request);
        Task<List<Subscription>> GetExpensiveSubs();
        Task<Subscription> CreateSubscription(Subscription subscription);
    }
}