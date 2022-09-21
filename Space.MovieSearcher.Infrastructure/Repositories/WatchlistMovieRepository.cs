﻿using Microsoft.EntityFrameworkCore;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;

namespace Space.MovieSearcher.Infrastructure.Repositories;

public class WatchlistMovieRepository : IWatchlistMovieRepository
{
    private readonly DbSet<WatchlistMovie> _dbSet;

    public WatchlistMovieRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<WatchlistMovie>();
    }

    public WatchlistMovie Add(WatchlistMovie movie)
    {
        return _dbSet.Add(movie).Entity;
    }

    public async Task<IReadOnlyList<WatchlistMovie>> GetAsync(int userId)
    {
        return await _dbSet.Include(movie => movie.Watchlist)
            .Where(movie => movie.Watchlist.UserId == userId)
            .ToArrayAsync();
    }

    public async Task<IReadOnlyList<WatchlistMovie>> GetAsync(string movieId, int userId)
    {
        return await _dbSet.Include(movie => movie.Watchlist)
            .Where(movie => movie.Watchlist.UserId == userId && movie.MovieId == movieId)
            .ToArrayAsync();
    }
}
