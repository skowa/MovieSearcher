namespace Space.MovieSearcher.Domain.Repositories;

public interface IWatchlistMovieRepository
{
    Task<IReadOnlyList<WatchlistMovie>> GetAsync(int userId);

    Task<IReadOnlyList<WatchlistMovie>> GetAsync(string movieId, int userId);

    WatchlistMovie Add(WatchlistMovie movie);
}