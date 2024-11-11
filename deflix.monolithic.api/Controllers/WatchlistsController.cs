using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistsController : ApiController
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistsController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetWatchlistForUser(int userId)
        {
            var watchlist = _watchlistService.GetWatchlistForUser(userId);
            if (watchlist == null || !watchlist.Any())
            {
                return NotFound(new { message = "No watchlist found for this user." });
            }

            return Ok(watchlist);
        }

        [HttpPost("user/{userId}/add/{movieId}")]
        public IActionResult AddToWatchlist(int userId, int movieId)
        {
            _watchlistService.AddToWatchlist(userId, movieId);
            return Ok(new { message = "Movie added to watchlist successfully." });
        }

        [HttpDelete("user/{userId}/remove/{movieId}")]
        public IActionResult RemoveFromWatchlist(int userId, int movieId)
        {
            _watchlistService.RemoveFromWatchlist(userId, movieId);
            return Ok(new { message = "Movie removed from watchlist successfully." });
        }
    }
}
