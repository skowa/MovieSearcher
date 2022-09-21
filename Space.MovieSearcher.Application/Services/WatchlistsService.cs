﻿using Space.MovieSearcher.Application.Models;
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

    public async Task AddMovieToWatchlistAsync(int userId, string movieId) // duplication
    {
        Watchlist watchlist = (await _watchlistRepository.GetAsync(userId)).SingleOrDefault()
            ?? new Watchlist { UserId = userId };

        _watchlistMovieRepository.Add(new WatchlistMovie
        {
            MovieId = movieId,
            Watchlist = watchlist
        });

        await _uow.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<WatchlistMovieModel>> GetMoviesAsync(int userId)
    {
        return (await _watchlistMovieRepository.GetAsync(userId))
            .Select(watchlistMovie => new WatchlistMovieModel
            {
                IsMovieWatched = watchlistMovie.IsMovieWatched,
                MovieId = watchlistMovie.MovieId
            })
            .ToArray();
    }

    public async Task MarkMovieAsWatchedAsync(int userId, string movieId)
    {
        WatchlistMovie watchlistMovie = (await _watchlistMovieRepository.GetAsync(movieId, userId))
            .SingleOrDefault();
        if (watchlistMovie == null)
        {
            throw new ArgumentException($"Invalid watchlist for user {userId}"); //
        }

        watchlistMovie.IsMovieWatched = true;
        await _uow.SaveChangesAsync();
    }
}
