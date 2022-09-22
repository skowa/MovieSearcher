namespace Space.MovieSearcher.Infrastructure.Services;

public interface IOffersService
{
    Task SendEmailOffersAsync(CancellationToken cancellationToken = default);
}
