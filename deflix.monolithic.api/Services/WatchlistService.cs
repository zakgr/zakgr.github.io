using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Helpers;
using deflix.monolithic.api.Interfaces;
using System.Data.SqlClient;

namespace deflix.monolithic.api.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IDatabaseService _databaseService;

        public WatchlistService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<WatchlistItemDto> GetWatchlistForUser(int userId)
        {
            var watchlist = new List<WatchlistItemDto>();

            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetWatchlistForUserQuery(),
                new SqlParameter("@UserId", userId)))
            {
                while (reader.Read())
                {
                    watchlist.Add(new WatchlistItemDto
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

            return watchlist;
        }

        public void AddToWatchlist(int userId, int movieId)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.InsertWatchlistItemQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@MovieId", movieId)
            );
        }

        public void RemoveFromWatchlist(int userId, int movieId)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.DeleteWatchlistItemQuery(),
                new SqlParameter("@UserId", userId),
                new SqlParameter("@MovieId", movieId)
            );
        }
    }
}
