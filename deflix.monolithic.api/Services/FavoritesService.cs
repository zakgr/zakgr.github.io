using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Helpers;
using deflix.monolithic.api.Interfaces;
using System.Data.SqlClient;

namespace deflix.monolithic.api.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IDatabaseService _databaseService;

        public FavoritesService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<FavoriteDto> GetFavoritesForUser(int userId)
        {
            var favorites = new List<FavoriteDto>();

            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetFavoritesForUserQuery(),
                new SqlParameter("@UserId", userId)))
            {
                while (reader.Read())
                {
                    favorites.Add(new FavoriteDto
                    {
                        MovieId = (int)reader["MovieId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Poster = reader["Poster"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        UsersRating = (float)reader["UsersRating"],
                        PlanType = reader["PlanType"].ToString()
                    });
                }
            }

            return favorites;
        }

        public void AddFavorite(int userId, int movieId)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.InsertFavoriteQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@MovieId", movieId)
            );
        }

        public void RemoveFavorite(int userId, int movieId)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.DeleteFavoriteQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@MovieId", movieId)
            );
        }
    }
}
