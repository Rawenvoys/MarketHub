using RB.SharedKernel.MediatR.Query;
namespace MarketHub.Microservices.Rates.Application.Queries.GetLastTable;

public record Query : IQuery<Result>;