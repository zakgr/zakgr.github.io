using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : ApiController
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetRecommendationsForUser(int userId)
        {
            var recommendations = _recommendationService.GetRecommendationsForUser(userId).ToList();
            if (recommendations == null || !recommendations.Any())
            {
                return NotFound(new { message = "No recommendations available for this user." });
            }

            return Ok(recommendations);
        }
    }

}
