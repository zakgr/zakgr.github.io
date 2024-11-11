namespace deflix.monolithic.api.Types
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PaymentMethod { get; set; }
    }

}
