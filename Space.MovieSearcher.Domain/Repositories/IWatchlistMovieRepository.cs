using System.Linq.Expressions;

namespace Space.MovieSearcher.Domain.Repositories;

public interface IWatchlistMovieRepository
{
    WatchlistMovie Add(WatchlistMovie movie);

    Task<IReadOnlyList<WatchlistMovie>> GetAsync(Expression<Func<WatchlistMovie, bool>> filter, CancellationToken cancellationToken = default);
}