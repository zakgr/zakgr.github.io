namespace deflix.monolithic.api.Interfaces
{
    using deflix.monolithic.api.DTOs;
    using System.Collections.Generic;

    public interface IRecommendationService
    {
        IEnumerable<RecommendationDto> GetRecommendationsForUser(int userId);
    }

}
