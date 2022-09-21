namespace Space.MovieSearcher.Domain;

public class WatchlistMovie
{
    public int Id { get; set; }

    public string MovieId { get; set; }

    public int WatchlistId { get; set; }

    public bool IsMovieWatched { get; set; }

    public Watchlist Watchlist { get; set; }
}
