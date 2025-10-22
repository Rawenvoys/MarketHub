using CurrencyRates.Clients.Nbp.Client;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Strategies;

public class NbpApiDateRangeSyncStrategy(INbpApi nbpApi, ILogger<NbpApiDateRangeSyncStrategy> logger) : ISyncStrategy
{
    private readonly INbpApi _nbpApi = nbpApi;
    private readonly ILogger<NbpApiDateRangeSyncStrategy> _logger = logger;
    public async Task ExecuteAsync(Guid sourceId)
    {
        _logger.LogInformation("Starting NBP date range sync for SourceId: {SourceId}", sourceId);
        throw new NotImplementedException();
    }
}
