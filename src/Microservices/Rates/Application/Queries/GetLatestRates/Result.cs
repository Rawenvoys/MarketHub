using CurrencyRates.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;
using RB.SharedKernel.MediatR.Query;
namespace CurrencyRates.Microservices.Rates.Application.Queries.GetLatestRates;

public record Result(CurrencyRateTableDto CurrencyRateTable) : IQueryResult;
