namespace Space.MovieSearcher.Domain.Repositories;

public interface IWatchlistRepository
{
    Task<IReadOnlyList<Watchlist>> GetAsync(int userId);
}