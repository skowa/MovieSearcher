using Microsoft.AspNetCore.Mvc;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Services.Contracts;
using Space.MovieSearcher.Presentation.Api.Models;

namespace Space.MovieSearcher.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class WatchlistsController : ControllerBase
{
    private readonly IWatchlistsService _watchlistsService;

    public WatchlistsController(IWatchlistsService watchlistsService)
    {
        _watchlistsService = watchlistsService;
    }

    [HttpGet("movies")]
    [ProducesResponseType(typeof(IReadOnlyList<WatchlistMovieModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<WatchlistMovieModel>>> GetWatchlistMovies(int userId, CancellationToken cancellationToken)
    {
        IReadOnlyList<WatchlistMovieModel> movies = await _watchlistsService.GetMoviesAsync(userId, cancellationToken);

        return Ok(movies);
    }

    [HttpPut("movies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddMovieToWatchlist(WatchlistMovieAddRequest request, CancellationToken cancellationToken)
    {
        await _watchlistsService.AddMovieToWatchlistAsync(request.UserId, request.MovieId, cancellationToken);

        return Ok();
    }

    [HttpPut("movies/{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkMovieAsWatched(int userId, string movieId, CancellationToken cancellationToken)
    {
        await _watchlistsService.MarkMovieAsWatchedAsync(userId, movieId, cancellationToken);

        return Ok();
    }
}