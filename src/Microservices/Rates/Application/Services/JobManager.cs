using System.Configuration;
using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class JobManager(ISourceRepository sourceRepository,
                        IConfiguration configuration,
                        ILogger<JobManager> logger,
                        ISyncStateRepository syncStateRepository,
                        ISourceSyncJobManager sourceSyncJobManager) : IHostedService
{
    private readonly ISourceRepository _sourceRepository = sourceRepository;
    private readonly ISyncStateRepository _syncStateRepository = syncStateRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<JobManager> _logger = logger;
    private readonly ISourceSyncJobManager _sourceSyncJobManager = sourceSyncJobManager;

    private const string SourceSyncArchiveCronExpressionSectionName = "SourceSyncArchiveCronExpression";

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Start of register source synchronization jobs");

        var sourceSyncArchiveCronExpression = _configuration.GetSection(SourceSyncArchiveCronExpressionSectionName)?.Value
            ?? throw new ConfigurationErrorsException($"Cannot find value for '{SourceSyncArchiveCronExpressionSectionName}' in configuration");

        var activeSources = await _sourceRepository.GetActiveAsync();
        if (activeSources is null || !activeSources.Any())
        {
            _logger.LogInformation("Cannot register source synchronization job. End of initialization");
            return;
        }

        activeSources.ToList().ForEach(async source =>
        {
            _logger.LogInformation("Configure source synchronization job for source: {SourceId}", source.Id);
            var syncState = await _syncStateRepository.GetAsync(source.Id, CancellationToken.None);
            await _sourceSyncJobManager.RegisterAsync(source, sourceSyncArchiveCronExpression, syncState.ArchiveSynchronized);
        });

        _logger.LogInformation("End of register source synchronization jobs");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
