using Microsoft.Extensions.Options;
using Space.MovieSearcher.Application.Options;
using Space.MovieSearcher.Application.Providers;
using Space.MovieSearcher.Application.Providers.Models;
using Space.MovieSearcher.Application.Services.Contracts;
using Space.MovieSearcher.Domain;
using Space.MovieSearcher.Domain.Repositories;

namespace Space.MovieSearcher.Application.Services;

public class OffersService : IOffersService
{
    private readonly EmailOfferOptions _emailOfferOptions;

    private readonly IWatchlistMovieRepository _watchlistMovieRepository;
    private readonly IEmailService _emailService;
    private readonly IImdbMoviesProvider _imdbMoviesProvider;
    private readonly IUnitOfWork _uow;

    public OffersService(
        IWatchlistMovieRepository watchlistMovieRepository,
        IEmailService emailService,
        IImdbMoviesProvider imdbMoviesProvider,
        IUnitOfWork uow,
        IOptions<EmailOfferOptions> emailOfferOptions)
    {
        _watchlistMovieRepository = watchlistMovieRepository;
        _emailService = emailService;
        _imdbMoviesProvider = imdbMoviesProvider;
        _uow = uow;
        _emailOfferOptions = emailOfferOptions.Value;
    }

    public async Task SendEmailOffersAsync(CancellationToken cancellationToken = default)
    {
        var usersMovies = await GetUsersUnwatchedMoviesAsync(cancellationToken);

        foreach (var userMovies in usersMovies)
        {
            var offer = await GetMovieToOfferAsync(userMovies.Value, cancellationToken);
            if (offer is not null)
            {
                await _emailService.SendAsync(
                    _emailOfferOptions.ReceivingEmail,
                    "Movie offer",
                    $"<h1>{offer.Title} (Imdb rating: {offer.ImdbRating})</h1></br>{offer.Wikipedia.PlotShort.Html}</br><img src='{offer.Image}' height='600' width='400'>",
                    cancellationToken
                );

                var watchlistMovie = userMovies.Value.FirstOrDefault(movie => movie.MovieId == offer.Id);
                if (watchlistMovie is not null)
                {
                    watchlistMovie.LastOfferDateTime = DateTime.UtcNow;
                    await _uow.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }

    private async Task<Dictionary<int, List<WatchlistMovie>>> GetUsersUnwatchedMoviesAsync(CancellationToken cancellationToken)
    {
        var unwatchedWatchlistMovies = await _watchlistMovieRepository.GetAsync(movie => !movie.IsMovieWatched, cancellationToken);

        return unwatchedWatchlistMovies
            .GroupBy(movie => movie.Watchlist.UserId)
            .Where(userMovies => userMovies.Count() > _emailOfferOptions.MinimumUnwatchedMoviesAmount)
            .ToDictionary(userMovies => userMovies.Key, userMovies => userMovies.ToList());
    }

    private async Task<ImdbMovieDetails> GetMovieToOfferAsync(IReadOnlyList<WatchlistMovie> userMovies, CancellationToken cancellationToken)
    {
        // Amount of days depends on business requirements, that are unclear from tech task description.
        // Error handling could be introduced here to avoid issues with IMDB API.
        // Caching could be introduced.
        var moviesDetails =
            await Task.WhenAll(userMovies
                .Where(movie => movie.LastOfferDateTime == null
                                || movie.LastOfferDateTime < DateTime.UtcNow - TimeSpan.FromDays(_emailOfferOptions.OfferDelayInDays))
                .Select(movie => _imdbMoviesProvider.GetMovieDetailsAsync(movie.MovieId, cancellationToken)));

        return moviesDetails.Where(movie => movie is not null).MaxBy(movie => movie.ImdbRating);
    }
}
