using System.Linq.Expressions;

namespace Space.MovieSearcher.Domain.Repositories;

public interface IWatchlistRepository
{
    Task<IReadOnlyList<Watchlist>> GetAsync(Expression<Func<Watchlist, bool>> filter, CancellationToken cancellationToken = default);
}