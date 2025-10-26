using MarketHub.Clients.Rates.Contracts.Dtos.GetLastTable;
using RB.SharedKernel.MediatR.Query;
namespace MarketHub.Microservices.Rates.Application.Queries.GetLastTable;

public record Result(TableDto Table) : IQueryResult;
