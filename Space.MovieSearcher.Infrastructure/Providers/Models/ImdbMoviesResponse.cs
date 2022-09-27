using Space.MovieSearcher.Application.Providers.Models;

namespace Space.MovieSearcher.Infrastructure.Models;

public class ImdbMoviesResponse
{
    public IReadOnlyList<ImdbMovie> Results { get; set; }

    public string Expression { get; set; }

    public string ErrorMessage { get; set; }
}
