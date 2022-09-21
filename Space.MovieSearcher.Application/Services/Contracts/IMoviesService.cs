using Space.MovieSearcher.Application.Models;

namespace Space.MovieSearcher.Application.Services.Contracts;

public interface IMoviesService
{
    Task<IReadOnlyList<MovieModel>> GetAsync(string title);
}