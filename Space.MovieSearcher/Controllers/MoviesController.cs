using Microsoft.AspNetCore.Mvc;
using Space.MovieSearcher.Application.Models;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Presentation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;

    public MoviesController(IMoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string title)
    {
        IEnumerable<MovieModel> movies = await _moviesService.GetAsync(title);

        return Ok(movies);
    }
}