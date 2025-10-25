using MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;
using RB.SharedKernel.MediatR.Query;
namespace MarketHub.Microservices.Rates.Application.Queries.GetLatestRates;

public record Result(CurrencyRateTableDto CurrencyRateTable) : IQueryResult;
