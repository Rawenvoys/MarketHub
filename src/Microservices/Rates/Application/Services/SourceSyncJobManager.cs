using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using Hangfire;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class SourceSyncJobManager(IRecurringJobManager recurringJobManager) : ISourceSyncJobManager
{
    private const string Archive = "Archive";
    private const string Actual = "Actual";
    private readonly IRecurringJobManager _recurringJobManager = recurringJobManager;

    public Task RegisterAsync(Source source, string defaultCronExpression, bool archiveSynchronized)
    {
        string jobId = BuildJobId(source.Id, source.SyncStrategy.Name, archiveSynchronized);
        // if (archiveSynchronized)
        //     _recurringJobManager.AddOrUpdate<IActualSourceSyncService>(jobId, ss => ss.ExecuteAsync(source.Id, source.SyncStrategy.Value, CancellationToken.None), source.Cron.Expression);
        // else
        _recurringJobManager.AddOrUpdate<IArchiveSourceSyncService>(jobId, ss => ss.ExecuteAsync(source.Id, source.SyncStrategy.Value, CancellationToken.None), defaultCronExpression);
        return Task.CompletedTask;
    }

    private static string BuildJobId(Guid id, string syncStrategyName, bool archiveSynchronized)
    {
        var syncModeName = archiveSynchronized ? Actual : Archive;
        var jobId = $"{syncStrategyName}-{syncModeName}Sync-{id}";
        return jobId;
    }
}
