namespace Space.MovieSearcher.Presentation.Api.Models;

/// <summary>
/// Request model for changing movie to watched or unwatched.
/// </summary>
public class MarkMovieAsWatchedRequest
{
    /// <summary>
    /// Is movie watched.
    /// </summary>
    public bool IsWatched { get; set; }
}
