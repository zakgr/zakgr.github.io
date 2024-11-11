namespace deflix.monolithic.api.Types
{
    public class UserSubscription
    {
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

}
