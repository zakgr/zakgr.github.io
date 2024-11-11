namespace deflix.monolithic.api.Types
{
    public class Recommendation
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string? Reason { get; set; }  // Optional: Reason for recommendation (e.g., "Based on your favorites")
    }

}
