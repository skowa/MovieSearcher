namespace Space.MovieSearcher.Presentation.Api.Models;

/// <summary>
/// Request model for adding new movie to user's watchlist.
/// </summary>
public class WatchlistMovieAddRequest
{
    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Movie id.
    /// </summary>
    public string MovieId { get; set; }
}
