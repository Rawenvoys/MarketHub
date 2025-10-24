using System.Configuration;
using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class JobManager(IServiceScopeFactory serviceScopeFactory,
                        IConfiguration configuration,
                        ILogger<JobManager> logger) : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<JobManager> _logger = logger;

    private const string SourceSyncArchiveCronExpressionSectionName = "SourceSyncArchiveCronExpression";

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Start of register source synchronization jobs");

        var sourceSyncArchiveCronExpression = _configuration.GetSection(SourceSyncArchiveCronExpressionSectionName)?.Value
            ?? throw new ConfigurationErrorsException($"Cannot find value for '{SourceSyncArchiveCronExpressionSectionName}' in configuration");



        using var scope = _serviceScopeFactory.CreateScope();
        var sourceRepository = scope.ServiceProvider.GetRequiredService<ISourceRepository>();
        var syncStateRepository = scope.ServiceProvider.GetRequiredService<ISyncStateRepository>();
        var sourceSyncJobManager = scope.ServiceProvider.GetRequiredService<ISourceSyncJobManager>();

        var activeSources = await sourceRepository.GetActiveAsync(cancellationToken);
        if (activeSources is null || !activeSources.Any())
        {
            _logger.LogInformation("Cannot register source synchronization job. End of initialization");
            return;
        }

        foreach (var source in activeSources)
        {
            _logger.LogInformation("Configure source synchronization job for source: {SourceId}", source.Id);
            var syncState = await syncStateRepository.GetAsync(source.Id, CancellationToken.None);
            await sourceSyncJobManager.RegisterAsync(source, sourceSyncArchiveCronExpression, syncState.ArchiveSynchronized);
        }

        _logger.LogInformation("End of register source synchronization jobs");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
