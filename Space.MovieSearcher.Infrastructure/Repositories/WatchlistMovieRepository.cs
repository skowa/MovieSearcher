using Microsoft.EntityFrameworkCore;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;
using System.Linq.Expressions;

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

    public async Task<IReadOnlyList<WatchlistMovie>> GetAsync(Expression<Func<WatchlistMovie, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(movie => movie.Watchlist)
            .Where(filter)
            .ToArrayAsync(cancellationToken);
    }
}
