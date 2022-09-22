using Microsoft.Extensions.Options;
using Space.MovieSearcher.Application.Providers;
using Space.MovieSearcher.Application.Providers.Models;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;
using Space.MovieSearcher.Infrastructure.Options;

namespace Space.MovieSearcher.Infrastructure.Services;

public class OffersService : IOffersService
{
    private const int OfferDelayInDays = 31;
    private const int MinimumUnwatchedMoviesAmount = 3;

    private readonly SmtpSettings _smtpSettings;

    private readonly IWatchlistMovieRepository _watchlistMovieRepository;
    private readonly IEmailService _emailService;
    private readonly IImdbMoviesProvider _imdbMoviesProvider;
    private readonly IUnitOfWork _uow;

    public OffersService(
        IWatchlistMovieRepository watchlistMovieRepository,
        IEmailService emailService,
        IImdbMoviesProvider imdbMoviesProvider,
        IUnitOfWork uow,
        IOptions<SmtpSettings> smtpSettings)
    {
        _watchlistMovieRepository = watchlistMovieRepository;
        _emailService = emailService;
        _imdbMoviesProvider = imdbMoviesProvider;
        _uow = uow;
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailOffersAsync()
    {
        IReadOnlyList<WatchlistMovie> unwatchedWatchlistMovies = await _watchlistMovieRepository.GetUnwatchedMoviesAsync();

        IDictionary<int, List<WatchlistMovie>> usersMovies = unwatchedWatchlistMovies
            .GroupBy(movie => movie.Watchlist.UserId)
        .Where(userMovies => userMovies.Count() > MinimumUnwatchedMoviesAmount)
            .ToDictionary(userMovies => userMovies.Key, userMovies => userMovies.ToList());

        foreach (var userMovies in usersMovies)
        {
            IReadOnlyList<ImdbMovieDetails> moviesDetails = await GetMoviesDetailsAsync(userMovies.Value);

            var offer = moviesDetails.MaxBy(movie => movie.ImdbRating);
            if (offer is not null)
            {
                await _emailService.SendAsync(
                    _smtpSettings.ReceivingEmail,
                    "Movie offer",
                    $"<h1>{offer.Title} (Imdb rating: {offer.ImdbRating})</h1></br>{offer.Wikipedia.PlotShort.Html}</br><img src='{offer.Image}' height='600' width='400'>"
                );

                var watchlistMovie = userMovies.Value.FirstOrDefault(movie => movie.MovieId == offer.Id);
                if (watchlistMovie is not null)
                {
                    watchlistMovie.LastOfferDateTime = DateTime.UtcNow;
                    await _uow.SaveChangesAsync();
                }
            }
        }
    }

    private async Task<IReadOnlyList<ImdbMovieDetails>> GetMoviesDetailsAsync(IReadOnlyList<WatchlistMovie> watchlistMovies)
    {
        var moviesDetails = new List<ImdbMovieDetails>(watchlistMovies.Count);
        foreach (WatchlistMovie movie in watchlistMovies)
        {
            // Amount of days depends on business requirements, that are unclear from tech task description.
            if (movie.LastOfferDateTime < DateTime.UtcNow - TimeSpan.FromDays(OfferDelayInDays))
            {
                moviesDetails.Add(await _imdbMoviesProvider.GetMovieDetailsAsync(movie.MovieId));
            }
        }

        return moviesDetails;
    }
}
