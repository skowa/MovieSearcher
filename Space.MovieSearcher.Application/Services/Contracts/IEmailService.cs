namespace Space.MovieSearcher.Application.Services.Contracts;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string htmlMessage, CancellationToken cancellationToken = default);
}