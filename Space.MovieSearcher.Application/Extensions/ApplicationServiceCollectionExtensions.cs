using Microsoft.Extensions.DependencyInjection;
using Space.MovieSearcher.Application.Options;
using Space.MovieSearcher.Application.Services;
using Space.MovieSearcher.Application.Services.Contracts;

namespace Space.MovieSearcher.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        Action<EmailOfferOptions> configure)
    {
        services.Configure(configure);

        services.AddScoped<IMoviesService, MoviesService>();
        services.AddScoped<IWatchlistsService, WatchlistsService>();
        services.AddScoped<IOffersService, OffersService>();

        return services;
    }
}
