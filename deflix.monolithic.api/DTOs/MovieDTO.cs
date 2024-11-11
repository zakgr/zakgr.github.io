namespace deflix.monolithic.api.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public string YoutubeKey { get; set; }
        public float UsersRating { get; set; }
        public string UsersComment { get; set; }
        public float CriticsRating { get; set; }
        public string PlanType { get; set; }
    }

    public class AddMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public string YoutubeKey { get; set; }
        public float UsersRating { get; set; }
        public string UsersComment { get; set; }
        public float CriticsRating { get; set; }
        public string PlanType { get; set; }
    }

}
