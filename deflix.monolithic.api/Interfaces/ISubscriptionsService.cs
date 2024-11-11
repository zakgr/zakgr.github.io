using deflix.monolithic.api.DTOs;

namespace deflix.monolithic.api.Interfaces
{
    public interface ISubscriptionsService
    {
        IEnumerable<SubscriptionDto> GetAllSubscriptions();
        UserSubscriptionDto GetUserSubscription(int userId);
        void SubscribeUser(int userId, int subscriptionId);
    }

}
