using Microsoft.AspNetCore.Mvc;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Presentation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    /// <summary>
    /// Gets all movies by title.
    /// </summary>
    /// <param name="title">Movie title.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Movies by <paramref name="title"/>.</returns>
    [HttpGet("{title}")]
    [ProducesResponseType(typeof(IReadOnlyList<MovieModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(string title, CancellationToken cancellationToken)
    {
        var movies = await _moviesService.GetAsync(title, cancellationToken);

        return Ok(movies);
    }
}