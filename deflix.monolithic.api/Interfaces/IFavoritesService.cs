namespace deflix.monolithic.api.Interfaces
{
    using deflix.monolithic.api.DTOs;
    using System.Collections.Generic;

    public interface IFavoritesService
    {
        IEnumerable<FavoriteDto> GetFavoritesForUser(int userId);
        void AddFavorite(int userId, int movieId);
        void RemoveFavorite(int userId, int movieId);
    }

}
