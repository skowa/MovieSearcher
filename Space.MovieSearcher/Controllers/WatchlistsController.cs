using Microsoft.AspNetCore.Mvc;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WatchlistsController : ControllerBase
{
    private readonly IWatchlistsService _watchlistsService;

    public WatchlistsController(IWatchlistsService watchlistsService)
    {
        _watchlistsService = watchlistsService;
    }

    [HttpGet("movies")]
    public async Task<ActionResult<IReadOnlyList<WatchlistMovieModel>>> GetWatchlistMovies(int userId)
    {
        IEnumerable<WatchlistMovieModel> movies = await _watchlistsService.GetMoviesAsync(userId);

        return Ok(movies);
    }

    [HttpPut("movies")]
    public async Task<IActionResult> AddMovieToWatchlist(int userId, string movieId)
    {
        await _watchlistsService.AddMovieToWatchlistAsync(userId, movieId);

        return Ok();
    }

    [HttpPut("movies/{movieId}")]
    public async Task<IActionResult> MarkMovieAsWatched(int userId, string movieId)
    {
        await _watchlistsService.MarkMovieAsWatchedAsync(userId, movieId);

        return Ok();
    }
}