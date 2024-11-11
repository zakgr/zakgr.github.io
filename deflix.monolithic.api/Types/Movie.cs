namespace deflix.monolithic.api.Types
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }  // URL to the movie poster image
        public string Genre { get; set; }
        public string YoutubeKey { get; set; }
        public float UsersRating { get; set; }
        public string UsersComment { get; set; }
        public float CriticsRating { get; set; }
        public string PlanType { get; set; }  // e.g., "Free", "Basic", "Premium"
    }

}
