namespace deflix.monolithic.api.Services
{
    using deflix.monolithic.api.DTOs;
    using deflix.monolithic.api.Helpers;
    using deflix.monolithic.api.Interfaces;
    using System.Collections.Generic;
    using System.Data.SqlClient;


    public class MovieService : IMovieService
    {
        private readonly IDatabaseService _databaseService;

        public MovieService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public IEnumerable<MovieDto> GetAllMovies()
        {
            var movies = new List<MovieDto>();

            using (var reader = _databaseService.ExecuteReader(SqlQueries.GetAllMoviesQuery()))
            {
                while (reader.Read())
                {
                    movies.Add(MapToMovieDto(reader));
                }
            }

            return movies;
        }

        public MovieDto GetMovieById(int id)
        {
            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetMovieByIdQuery(),
                new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    return MapToMovieDto(reader);
                }
            }

            return null;
        }

        public void AddMovie(AddMovieDto movieDto)
        {
            _databaseService.ExecuteNonQuery(
                SqlQueries.InsertMovieQuery(),
                new SqlParameter("@Title", movieDto.Title),
                new SqlParameter("@Description", movieDto.Description),
                new SqlParameter("@Poster", movieDto.Poster),
                new SqlParameter("@Genre", movieDto.Genre),
                new SqlParameter("@YoutubeKey", movieDto.YoutubeKey),
                new SqlParameter("@UsersRating", movieDto.UsersRating),
                new SqlParameter("@UsersComment", movieDto.UsersComment),
                new SqlParameter("@CriticsRating", movieDto.CriticsRating),
                new SqlParameter("@PlanType", movieDto.PlanType)
            );
        }

        public IEnumerable<MovieDto> GetMoviesByGenre(string genre)
        {
            var movies = new List<MovieDto>();

            using (var reader = _databaseService.ExecuteReader(
                SqlQueries.GetMoviesByGenreQuery(),
                new SqlParameter("@Genre", genre)))
            {
                while (reader.Read())
                {
                    movies.Add(MapToMovieDto(reader));
                }
            }

            return movies;
        }

        private MovieDto MapToMovieDto(SqlDataReader reader)
        {
            return new MovieDto
            {
                Id = (int)reader["Id"],
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                Poster = reader["Poster"].ToString(),
                Genre = reader["Genre"].ToString(),
                YoutubeKey = reader["YoutubeKey"].ToString(),
                UsersRating = (float)reader["UsersRating"],
                UsersComment = reader["UsersComment"].ToString(),
                CriticsRating = (float)reader["CriticsRating"],
                PlanType = reader["PlanType"].ToString()
            };
        }
    }

}
