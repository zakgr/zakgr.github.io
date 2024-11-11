using deflix.monolithic.api.DTOs;

namespace deflix.monolithic.api.Interfaces
{
    public interface IWatchlistService
    {
        IEnumerable<WatchlistItemDto> GetWatchlistForUser(int userId);
        void AddToWatchlist(int userId, int movieId);
        void RemoveFromWatchlist(int userId, int movieId);
    }
}
