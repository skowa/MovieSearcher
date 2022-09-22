using Space.MovieSearcher.Application.Models;

namespace Space.MovieSearcher.Application.Services.Contracts;

public interface IWatchlistsService
{
    Task<IReadOnlyList<WatchlistMovieModel>> GetMoviesAsync(int userId, CancellationToken cancellationToken = default);

    Task AddMovieToWatchlistAsync(int userId, string movieId, CancellationToken cancellationToken = default);

    Task MarkMovieAsWatchedAsync(int userId, string movieId, CancellationToken cancellationToken = default);
}
