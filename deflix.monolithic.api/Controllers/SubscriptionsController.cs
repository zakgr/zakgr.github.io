using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpGet("list")]
        public IActionResult GetAllSubscriptions()
        {
            var subscriptions = _subscriptionsService.GetAllSubscriptions();
            return Ok(subscriptions);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserSubscription(int userId)
        {
            var subscription = _subscriptionsService.GetUserSubscription(userId);
            if (subscription == null)
            {
                return NotFound(new { message = "No subscription found for this user." });
            }

            return Ok(subscription);
        }

        [HttpPost("user/{userId}/subscribe/{subscriptionId}")]
        public IActionResult SubscribeUser(int userId, int subscriptionId)
        {
            _subscriptionsService.SubscribeUser(userId, subscriptionId);
            return Ok(new { message = "User subscribed successfully." });
        }
    }
}
