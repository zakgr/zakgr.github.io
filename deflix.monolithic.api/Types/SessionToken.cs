namespace deflix.monolithic.api.Types
{
    public class SessionToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
