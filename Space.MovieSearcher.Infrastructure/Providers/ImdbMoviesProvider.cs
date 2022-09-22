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
    private const string MovieDetailsPath = "/{0}/API/Title/{1}/{2}/Wikipedia";

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
        var response = await _httpClient.GetFromJsonAsync<ImdbMoviesResponse>(path);
        if (!string.IsNullOrWhiteSpace(response?.ErrorMessage))
        {
            return Array.Empty<ImdbMovie>();
        }

        return response?.Results ?? Array.Empty<ImdbMovie>();
    }

    public async Task<ImdbMovieDetails> GetMovieDetailsAsync(string id)
    {
        var path = string.Format(MovieDetailsPath, _imdbApiOptions.Language, _imdbApiOptions.ApiKey, id);

        var movieDetails = await _httpClient.GetFromJsonAsync<ImdbMovieDetails>(path);
        if (!string.IsNullOrWhiteSpace(movieDetails.ErrorMessage))
        {
            return null;
        }

        return movieDetails;
    }
}
