using Microsoft.Extensions.Options;
using Space.MovieSearcher.Application.Providers;
using Space.MovieSearcher.Application.Providers.Models;
using Space.MovieSearcher.Infrastructure.Models;
using Space.MovieSearcher.Infrastructure.Options;
using System.Net.Http.Json;

namespace Space.MovieSearcher.Infrastructure.Providers;

public class ImdbMoviesProvider : IImdbMoviesProvider
{
    private const string SearchPath = "/{0}/API/Search/{1}/{2}";

    private readonly HttpClient _httpClient;
    private readonly ImdbApiOptions _imdbApiOptions;

    public ImdbMoviesProvider(HttpClient httpClient, IOptions<ImdbApiOptions> imdbApiOptions)
    {
        _httpClient = httpClient;
        _imdbApiOptions = imdbApiOptions.Value;
    }

    public async Task<IReadOnlyList<ImdbMovie>> GetAsync(string title)
    {
        var path = string.Format(SearchPath, _imdbApiOptions.Language, _imdbApiOptions.ApiKey, title);

        return (await _httpClient.GetFromJsonAsync<ImdbMoviesResponse>(path))?.Results ?? Array.Empty<ImdbMovie>();
    }
}
