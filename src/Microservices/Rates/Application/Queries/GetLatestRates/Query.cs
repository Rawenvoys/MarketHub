using RB.SharedKernel.MediatR.Query;
namespace MarketHub.Microservices.Rates.Application.Queries.GetLatestRates;

public record Query : IQuery<Result>;