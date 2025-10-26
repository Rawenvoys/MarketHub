using MarketHub.Clients.Rates.Contracts.Dtos.GetLastTable;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using RB.SharedKernel.MediatR.Query;

namespace MarketHub.Microservices.Rates.Application.Queries.GetLastTable;

public class Handler(ITableRepository tableRepository) : IQueryHandler<Query, Result>
{
    private readonly ITableRepository _tableRepository = tableRepository;

    public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
    {
        var currencyRateTable = await _tableRepository.GetLatestAsync(cancellationToken);
        var currencyRateTableDto = new TableDto(currencyRateTable.Number.Value, currencyRateTable.EffectiveDate, [.. currencyRateTable.CurrencyRates.Select(cr => new CurrencyRateDto(cr.Currency.Name.Value, cr.Currency.Code.Value, cr.Mid.Value))]);
        return new Result(currencyRateTableDto);
    }
}
