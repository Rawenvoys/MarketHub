using MarketHub.Clients.Rates.Contracts.Dtos.GetMetaQuery;

namespace MarketHub.Clients.Rates.Client;

public interface IMetaApi
{
    [Get("/meta")]
    Task<MetaDto> GetMetaAsync(CancellationToken cancellationToken);
}
