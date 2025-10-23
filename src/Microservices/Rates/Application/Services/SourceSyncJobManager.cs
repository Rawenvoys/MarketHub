using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using Hangfire;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class SourceSyncJobManager(IRecurringJobManager recurringJobManager) : ISourceSyncJobManager
{
    private const string Archive = "Archive";
    private const string Actual = "Actual";
    private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;

    public async Task RegisterAsync(Guid id, string syncStrategyName, string sourceCronExpression, string defaultCronExpression, bool archiveSynchronized)
    {
        string jobId = BuildJobId(id, syncStrategyName, archiveSynchronized);
        if (archiveSynchronized)
            _recurringJobManager.AddOrUpdate<IActualSourceSyncService>(jobId, ss => ss.ExecuteAsync(CancellationToken.None), sourceCronExpression);
        else 
            _recurringJobManager.AddOrUpdate<IArchiveSourceSyncService>(jobId, ss => ss.ExecuteAsync(CancellationToken.None), defaultCronExpression);
    }

    private static string BuildJobId(Guid id, string syncStrategyName, bool archiveSynchronized) 
    {
        var syncModeName = archiveSynchronized ? Actual : Archive;
        var jobId = $"{syncStrategyName}-{syncModeName}Sync-{id}";
        return jobId;
    }
}
