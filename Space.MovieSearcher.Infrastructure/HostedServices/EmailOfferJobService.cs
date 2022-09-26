using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Space.MovieSearcher.Application.Services.Contracts;
using Space.MovieSearcher.Infrastructure.Options;

namespace Space.MovieSearcher.Infrastructure.HostedServices;

public class EmailOfferJobService : BackgroundService
{
    private readonly CronExpression _expression;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EmailOfferJobService> _logger;

    public EmailOfferJobService(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<EmailOfferJobSettings> emailOfferJobSettings,
        ILogger<EmailOfferJobService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _expression = CronExpression.Parse(emailOfferJobSettings.Value.CronSchedulingExpression, CronFormat.IncludeSeconds);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var nextJobRun = _expression.GetNextOccurrence(DateTime.UtcNow);
            await Task.Delay(nextJobRun.Value - DateTime.UtcNow, cancellationToken);

            _logger.LogInformation("Emails offering job started");
            using var scope = _serviceScopeFactory.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IOffersService>().SendEmailOffersAsync(cancellationToken);
            _logger.LogInformation("Emails offering job finished");
        }
    }
}
