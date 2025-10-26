using MarketHub.Clients.Rates.Contracts.Dtos.GetMetaQuery;
using RB.SharedKernel.MediatR.Query;

namespace MarketHub.Microservices.Rates.Application.Queries.GetMeta;

public record Result(MetaDto Meta) : IQueryResult;