using CurrencyRates.Clients.Nbp.Client;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;
using TableType = CurrencyRates.Microservices.Rates.Domain.Enums.Table.Type;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.States;
using Microsoft.Extensions.Logging;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Table;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Strategies;

public class NbpApiDateRangeSyncStrategy(INbpApi nbpApi, ILogger<NbpApiDateRangeSyncStrategy> logger, ISyncStateRepository syncStateRepository, ITableRepository tableRepository)
    : ISyncStrategy
{
    private readonly INbpApi _nbpApi = nbpApi;
    private readonly ILogger<NbpApiDateRangeSyncStrategy> _logger = logger;
    private readonly ISyncStateRepository _syncStateRepository = syncStateRepository;
    private readonly ITableRepository _tableRepository = tableRepository;

    private readonly TableType _tableType = TableType.B;

    //ToDo: Exception message for this...
    public async Task ExecuteAsync(Guid sourceId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting NBP date range sync for SourceId: {SourceId}", sourceId);
        var syncState = await _syncStateRepository.GetAsync(sourceId, cancellationToken);
        if (syncState is not NbpApiDateRangeSyncState dateRangeSyncState)
            throw new InvalidOperationException("");

        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (!dateRangeSyncState.ArchiveSynchronized)
            _logger.LogInformation("Synchronize archive data for SourceId: {SourceId}", sourceId);
        else
        {
            _logger.LogInformation("Synchronize actual data for SourceId: {SourceId}", sourceId);

            var fromDate = dateRangeSyncState.NextSyncFrom;
            var toDate = currentDate;

            var tables = await _nbpApi.Get(_tableType, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"), cancellationToken);
            tables?.Select(t => Table.Create(_tableType, Number.FromValue(t.No), t.EffectiveDate, sourceId)).ToList().ForEach(async t =>
            {
                await _tableRepository.SaveAsync(t, cancellationToken);
            });
        }

        _logger.LogInformation("End of NBP date range sync for SourceId: {SourceId}", sourceId);
    }
}
