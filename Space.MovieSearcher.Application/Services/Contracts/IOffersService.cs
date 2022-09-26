namespace Space.MovieSearcher.Application.Services.Contracts;

public interface IOffersService
{
    Task SendEmailOffersAsync(CancellationToken cancellationToken = default);
}
