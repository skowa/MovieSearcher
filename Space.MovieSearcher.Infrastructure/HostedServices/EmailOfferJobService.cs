using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Space.MovieSearcher.Infrastructure.Services;

namespace Space.MovieSearcher.Infrastructure.HostedServices;

public class EmailOfferJobService : BackgroundService
{
    private const string CronJobExpression = "0 30 19 ? * SUN";

    private readonly CronExpression _expression;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EmailOfferJobService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        _expression = CronExpression.Parse(CronJobExpression, CronFormat.IncludeSeconds);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            DateTime? nextJobRun = _expression.GetNextOccurrence(DateTime.UtcNow);
            await Task.Delay(nextJobRun.Value - DateTime.UtcNow, cancellationToken);

            using var scope = _serviceScopeFactory.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IOffersService>().SendEmailOffersAsync();
        }
    }
}
