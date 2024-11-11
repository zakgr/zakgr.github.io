using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ApiController
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetFavoritesForUser(int userId)
        {
            var favorites = _favoritesService.GetFavoritesForUser(userId);
            if (favorites == null || !favorites.Any())
            {
                return NotFound(new { message = "No favorites found for this user." });
            }

            return Ok(favorites);
        }

        [HttpPost("user/{userId}/add/{movieId}")]
        public IActionResult AddFavorite(int userId, int movieId)
        {
            _favoritesService.AddFavorite(userId, movieId);
            return Ok(new { message = "Movie added to favorites successfully." });
        }

        [HttpDelete("user/{userId}/remove/{movieId}")]
        public IActionResult RemoveFavorite(int userId, int movieId)
        {
            _favoritesService.RemoveFavorite(userId, movieId);
            return Ok(new { message = "Movie removed from favorites successfully." });
        }
    }
}
