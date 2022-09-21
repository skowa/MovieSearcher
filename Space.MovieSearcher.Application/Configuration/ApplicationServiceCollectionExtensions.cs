using Microsoft.Extensions.DependencyInjection;
using Space.MovieSearcher.Application.Services;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Application.Configuration;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMoviesService, MoviesService>();
        services.AddScoped<IWatchlistsService, WatchlistsService>();

        return services;
    }
}
