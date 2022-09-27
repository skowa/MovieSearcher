using Space.MovieSearcher.Application.Exceptions;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Services.Contracts;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;

namespace Space.MovieSearcher.Application.Services;

public class WatchlistsService : IWatchlistsService
{
    private readonly IUnitOfWork _uow;
    private readonly IWatchlistMovieRepository _watchlistMovieRepository;
    private readonly IWatchlistRepository _watchlistRepository;

    public WatchlistsService(
        IUnitOfWork uow,
        IWatchlistMovieRepository watchlistMovieRepository,
        IWatchlistRepository watchlistRepository)
    {
        _uow = uow;
        _watchlistMovieRepository = watchlistMovieRepository;
        _watchlistRepository = watchlistRepository;
    }

    public async Task AddMovieToWatchlistAsync(int userId, string movieId, CancellationToken cancellationToken = default)
    {
        var watchlistMovie = await GetWatchlistMovieAsync(userId, movieId, cancellationToken);
        if (watchlistMovie is not null)
        {
            throw new ArgumentException($"Movie {movieId} is duplicated");
        }

        var watchlist = await GetWatchlistAsync(userId, cancellationToken)
            ?? new Watchlist { UserId = userId };

        _watchlistMovieRepository.Add(new WatchlistMovie
        {
            MovieId = movieId,
            Watchlist = watchlist
        });

        await _uow.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WatchlistMovieModel>> GetMoviesAsync(int userId, CancellationToken cancellationToken = default)
    {
        var watchlist = await GetWatchlistAsync(userId, cancellationToken);
        if (watchlist is null)
        {
            throw new NotFoundException($"Watchlist for user {userId} is not found");
        }

        return (await _watchlistMovieRepository.GetAsync(movie => movie.WatchlistId == watchlist.Id, cancellationToken))
            .Select(watchlistMovie => new WatchlistMovieModel
            {
                IsMovieWatched = watchlistMovie.IsMovieWatched,
                MovieId = watchlistMovie.MovieId
            })
            .ToArray();
    }

    public async Task MarkMovieAsWatchedAsync(int userId, string movieId, bool isWatched, CancellationToken cancellationToken = default)
    {
        var watchlistMovie = await GetWatchlistMovieAsync(userId, movieId, cancellationToken);
        if (watchlistMovie is null)
        {
            throw new NotFoundException($"Movie {movieId} for user {userId} is not found");
        }

        watchlistMovie.IsMovieWatched = isWatched;

        await _uow.SaveChangesAsync(cancellationToken);
    }

    private async Task<WatchlistMovie> GetWatchlistMovieAsync(int userId, string movieId, CancellationToken cancellationToken = default)
    {
        return (await _watchlistMovieRepository.GetAsync(movie => movie.Watchlist.UserId == userId && movie.MovieId == movieId, cancellationToken))
            .SingleOrDefault();
    }

    private async Task<Watchlist> GetWatchlistAsync(int userId, CancellationToken cancellationToken = default)
    {
        return (await _watchlistRepository.GetAsync(watchlist => watchlist.UserId == userId, cancellationToken)).SingleOrDefault();
    }
}
