using Microsoft.EntityFrameworkCore;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;

namespace Space.MovieSearcher.Infrastructure.Repositories;

public class WatchlistRepository : IWatchlistRepository
{
    private readonly DbSet<Watchlist> _dbSet;

    public WatchlistRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<Watchlist>();
    }

    public Watchlist Add(Watchlist watchlist)
    {
        return _dbSet.Add(watchlist).Entity;
    }

    public async Task<IReadOnlyList<Watchlist>> GetAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(watchlist => watchlist.UserId == userId).ToArrayAsync(cancellationToken);
    }
}
