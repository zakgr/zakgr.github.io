namespace deflix.monolithic.api.Services
{
    using deflix.monolithic.api.DTOs;
    using deflix.monolithic.api.Helpers;
    using deflix.monolithic.api.Interfaces;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class RecommendationService : IRecommendationService
    {
        private readonly IDatabaseService _databaseService;

        public RecommendationService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<RecommendationDto> GetRecommendationsForUser(int userId)
        {
            var recommendations = new List<RecommendationDto>();

            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetRecommendationsForUserQuery(),
                new SqlParameter("@UserId", userId)))
            {
                while (reader.Read())
                {
                    recommendations.Add(new RecommendationDto
                    {
                        MovieId = (int)reader["MovieId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Poster = reader["Poster"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        UsersRating = (float)reader["UsersRating"],
                        PlanType = reader["PlanType"].ToString(),
                        Reason = reader["Reason"].ToString()  // Optional
                    });
                }
            }

            return recommendations;
        }
    }

}
