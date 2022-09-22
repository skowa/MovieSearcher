namespace Space.MovieSearcher.Infrastructure.Services;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string htmlMessage);
}