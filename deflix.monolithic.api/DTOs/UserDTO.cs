namespace deflix.monolithic.api.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserProfileUpdateDto
    {
        public string Email { get; set; }
        public string SubscriptionType { get; set; }
        public string PaymentMethod { get; set; }
    }

}
