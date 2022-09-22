using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Space.MovieSearcher.Application.Providers;
using Space.MovieSearcher.Domain.Repositories;
using Space.MovieSearcher.Infrastructure.HostedServices;
using Space.MovieSearcher.Infrastructure.Options;
using Space.MovieSearcher.Infrastructure.Providers;
using Space.MovieSearcher.Infrastructure.Repositories;
using Space.MovieSearcher.Infrastructure.Services;

namespace Space.MovieSearcher.Infrastructure.Configuration
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddImdbProvider(
            this IServiceCollection services,
            Action<ImdbApiOptions> configure)
        {
            services.Configure(configure);
            services.AddHttpClient<IImdbMoviesProvider, ImdbMoviesProvider>((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = serviceProvider.GetRequiredService<IOptions<ImdbApiOptions>>().Value.ImdbApi;
            });

            return services;
        }

        public static IServiceCollection AddInfrastrucutreServices(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<SmtpSettings> configure)
        {
            services.Configure(configure);

            services.AddDbContext<DbContext, MoviesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MoviesConnection")));

            services.AddScoped<IWatchlistRepository, WatchlistRepository>();
            services.AddScoped<IWatchlistMovieRepository, WatchlistMovieRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IOffersService, OffersService>();

            return services;
        }

        public static IServiceCollection AddHostedServices(
            this IServiceCollection services)
        {
            services.AddHostedService<EmailOfferJobService>();

            return services;
        }
    }
}
