using Space.MovieSearcher.Application.Models;

namespace Space.MovieSearcher.Application.Services.Contracts;

public interface IWatchlistsService
{
    Task<IReadOnlyList<WatchlistMovieModel>> GetMoviesAsync(int userId);

    Task AddMovieToWatchlistAsync(int userId, string movieId);

    Task MarkMovieAsWatchedAsync(int userId, string movieId);
}
