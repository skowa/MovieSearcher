namespace Space.MovieSearcher.Infrastructure.Options;

public class ImdbApiOptions
{
    public Uri ImdbApi { get; set; }

    public string ApiKey { get; set; }

    public string Language { get; set; }
}