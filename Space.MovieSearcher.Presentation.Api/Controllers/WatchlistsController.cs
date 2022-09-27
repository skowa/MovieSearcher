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

    /// <summary>
    /// Gets movies from user's watchlist.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User's watchlist items.</returns>
    [HttpGet("movies")]
    [ProducesResponseType(typeof(IReadOnlyList<WatchlistMovieModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetWatchlistMovies(int userId, CancellationToken cancellationToken)
    {
        // UserId can be also retrieved from JWT token.
        var movies = await _watchlistsService.GetMoviesAsync(userId, cancellationToken);

        return Ok(movies);
    }

    /// <summary>
    /// Adds movie to user's watchlist.
    /// </summary>
    /// <param name="request">WatchlistMovie details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("movies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddMovieToWatchlist(WatchlistMovieAddRequest request, CancellationToken cancellationToken)
    {
        await _watchlistsService.AddMovieToWatchlistAsync(request.UserId, request.MovieId, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Sets movie as watched or unwatched.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="movieId">Movie id.</param>
    /// <param name="request">Updated IsWatched field.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPatch("movies/{movieId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MarkMovieAsWatched(int userId, string movieId, MarkMovieAsWatchedRequest request, CancellationToken cancellationToken)
    {
        await _watchlistsService.MarkMovieAsWatchedAsync(userId, movieId, request.IsWatched, cancellationToken);

        return Ok();
    }
}