namespace Space.MovieSearcher.Domain.Repositories;

public interface IWatchlistMovieRepository
{
    Task<IReadOnlyList<WatchlistMovie>> GetAsync(int userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WatchlistMovie>> GetAsync(string movieId, int userId, CancellationToken cancellationToken = default);

    WatchlistMovie Add(WatchlistMovie movie);

    Task<IReadOnlyList<WatchlistMovie>> GetUnwatchedMoviesAsync(CancellationToken cancellationToken = default);
}