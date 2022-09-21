using Space.MovieSearcher.Application.Providers.Models;

namespace Space.MovieSearcher.Application.Providers;

public interface IImdbMoviesProvider
{
    Task<IReadOnlyList<ImdbMovie>> GetAsync(string title);
}