using MarketHub.Clients.Rates.Contracts.Dtos.GetLastTable;

namespace MarketHub.Clients.Rates.Client;

public interface ITablesApi
{
    [Get("/tables/last")]
    Task<TableDto> GetLastTableAsync(CancellationToken cancellationToken);
}
