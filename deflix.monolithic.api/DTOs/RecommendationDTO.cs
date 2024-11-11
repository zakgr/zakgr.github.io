﻿namespace deflix.monolithic.api.DTOs
{
    public class RecommendationDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public float UsersRating { get; set; }
        public string PlanType { get; set; }
        public string? Reason { get; set; }  // Optional
    }
}