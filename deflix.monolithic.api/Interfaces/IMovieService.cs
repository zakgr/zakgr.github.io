namespace deflix.monolithic.api.Interfaces
{
    using deflix.monolithic.api.DTOs;
    using System.Collections.Generic;

    public interface IMovieService
    {
        IEnumerable<MovieDto> GetAllMovies();
        MovieDto GetMovieById(int id);
        void AddMovie(AddMovieDto movieDto);
        IEnumerable<MovieDto> GetMoviesByGenre(string genre);
    }

}
