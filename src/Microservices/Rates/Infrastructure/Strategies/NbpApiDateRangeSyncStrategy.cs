using MarketHub.Clients.Nbp.Client;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Domain.Interfaces.Strategies;
using TableType = MarketHub.Microservices.Rates.Domain.Enums.Table.Type;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.States;
using Microsoft.Extensions.Logging;
using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.ValueObjects.Table;
using MarketHub.Microservices.Rates.Domain.Entities;
using MarketHub.Microservices.Rates.Domain.ValueObjects.Currency;
using MarketHub.Microservices.Rates.Domain.ValueObjects;

namespace MarketHub.Microservices.Rates.Infrastructure.Strategies;

public class NbpApiDateRangeSyncStrategy(INbpApi nbpApi, ILogger<NbpApiDateRangeSyncStrategy> logger, ISyncStateRepository syncStateRepository, ITableRepository tableRepository)
    : ISyncStrategy
{
    private readonly INbpApi _nbpApi = nbpApi;
    private readonly ILogger<NbpApiDateRangeSyncStrategy> _logger = logger;
    private readonly ISyncStateRepository _syncStateRepository = syncStateRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly TableType _tableType = TableType.B;

    //ToDo: Create DateRangeSyncState entity in domain
    public async Task ExecuteAsync(Guid sourceId, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = DateTime.UtcNow;
        _logger.LogInformation("[{ExecutionDateTime}]: Starting NBP date range sync for SourceId: {SourceId}", utcNow.ToString("yyyy-MM-dd hh:ss"), sourceId);
        var syncState = await _syncStateRepository.GetAsync(sourceId, cancellationToken);
        if (syncState is not NbpApiDateRangeSyncState dateRangeSyncState)
            throw new InvalidOperationException($"Expected NbpApiDateRangeSyncState for sourceId: {sourceId}, but got {syncState?.GetType().Name ?? "null"}");

        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (dateRangeSyncState.NextSyncAt > currentDate)
            return;

        var fromDate = dateRangeSyncState.NextSyncAt;
        DateOnly toDate;
        if (!dateRangeSyncState.ArchiveSynchronized)
        {
            var endOfQuarterDate = CalculateEndOfQuarterDate(fromDate);
            toDate = CalculateSyncDateLimit(currentDate, endOfQuarterDate);
            dateRangeSyncState.ArchiveSynchronized = toDate >= currentDate;
        }
        else
            toDate = currentDate;

        dateRangeSyncState.NextSyncAt = toDate.AddDays(1);

        _logger.LogDebug("Get Tables from {FromDate} to {ToDate} for {SourceId}", fromDate, toDate, sourceId);
        var tables = await _nbpApi.GetAsync(_tableType, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"), cancellationToken);

        foreach (var t in tables)
        {
            var table = Table.Create(_tableType, Number.FromValue(t.No), t.EffectiveDate, sourceId);
            var currencyRates = new List<CurrencyRate>();
            foreach (var rate in t.Rates)
            {
                var currency = Currency.Create(Code.FromValue(rate.Code), Name.FromValue(rate.Currency));
                var currencyRate = CurrencyRate.Create(table.Id, currency.Id, Rate.FromDecimal(rate.Mid));
                currencyRate.SetCurrency(currency);
                currencyRate.SetTable(table);
                currencyRates.Add(currencyRate);
            }
            table.AddCurrencyRates(currencyRates);
            await _tableRepository.AddAsync(table, cancellationToken);
        }
        await _syncStateRepository.SaveAsync(dateRangeSyncState, cancellationToken);
        _logger.LogInformation("End of NBP date range sync for SourceId: {SourceId}", sourceId);
    }

    private static DateOnly CalculateEndOfQuarterDate(DateOnly date)
    {
        var quarter = (date.Month - 1) / 3 + 1;
        var lastMonthOfQuarter = quarter * 3;
        return new DateOnly(date.Year, lastMonthOfQuarter, 1).AddMonths(1).AddDays(-1);
    }

    private static DateOnly CalculateSyncDateLimit(DateOnly currentDate, DateOnly endOfQuarterDate) => endOfQuarterDate > currentDate ? currentDate : endOfQuarterDate;
}



