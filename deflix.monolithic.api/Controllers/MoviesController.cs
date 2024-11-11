using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace deflix.monolithic.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ApiController
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("list")]
        public IActionResult GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("movie/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            return Ok(movie);
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] AddMovieDto movieDto)
        {
            _movieService.AddMovie(movieDto);
            return Ok(new { message = "Movie added successfully" });
        }

        [HttpGet("list/genre/{genre}")]
        public IActionResult GetMoviesByGenre(string genre)
        {
            var movies = _movieService.GetMoviesByGenre(genre);
            return Ok(movies);
        }
    }

}
