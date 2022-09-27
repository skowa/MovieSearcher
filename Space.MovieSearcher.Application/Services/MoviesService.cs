using Space.MovieSearcher.Application.Exceptions;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Providers;
using Space.MovieSearcher.Application.Providers.Models;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Application.Services;

public class MoviesService : IMoviesService
{
    private readonly IImdbMoviesProvider _imdbMoviesProvider;

    public MoviesService(IImdbMoviesProvider imdbMoviesProvider)
    {
        _imdbMoviesProvider = imdbMoviesProvider;
    }

    public async Task<IReadOnlyList<MovieModel>> GetAsync(string title, CancellationToken cancellationToken = default)
    {
        var movies = await _imdbMoviesProvider.GetAsync(title, cancellationToken);
        if (!movies.Any())
        {
            throw new NotFoundException($"Movies by title {title} are not found");
        }

        return movies
            .Select(imdbMovie => new MovieModel
            {
                Id = imdbMovie.Id,
                Description = imdbMovie.Description,
                Title = imdbMovie.Title,
                Poster = imdbMovie.Image
            })
            .ToArray();
    }
}
