using RB.SharedKernel.MediatR.Query;
namespace CurrencyRates.Microservices.Rates.Application.Queries.GetLatestRates;

public record Query : IQuery<Result>;