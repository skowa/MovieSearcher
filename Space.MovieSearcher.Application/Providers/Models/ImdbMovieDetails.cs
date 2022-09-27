namespace Space.MovieSearcher.Application.Providers.Models;

public class ImdbMovieDetails
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Image { get; set; }

    public string ImdbRating { get; set; }

    public ImdbMovieWikipediaDetails Wikipedia { get; set; }

    public string ErrorMessage { get; set; }
}
