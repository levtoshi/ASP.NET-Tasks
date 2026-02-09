using HomeWork_09._02._2026.Interfaces;
using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;
using HomeWork_09._02._2026.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork_09._02._2026.Controllers
{
    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("free-expired")]
        public async Task<IActionResult> GetFreeExpiredSubscriptions()
        {
            return Ok(await _subscriptionService.GetFreeExpiredSubscriptions());
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupedBy([FromQuery] SubscriptionRequest request)
        {
            List<SubscriptionTypeCountDto> subscriptions;

            try
            {
                subscriptions = await _subscriptionService.GetGroupedBy(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(subscriptions);
        }

        [HttpGet("expensive")]
        public async Task<IActionResult> GetExpensiveSubs()
        {
            return Ok(await _subscriptionService.GetExpensiveSubs());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription([FromBody] Subscription subscription)
        {
            Subscription createdSubscription;
            try
            {
                createdSubscription = await _subscriptionService.CreateSubscription(subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(createdSubscription);
        }
    }
}
