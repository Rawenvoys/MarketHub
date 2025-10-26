using MarketHub.Clients.Rates.Contracts.Dtos.GetMetaQuery;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using RB.SharedKernel.MediatR.Query;

namespace MarketHub.Microservices.Rates.Application.Queries.GetMeta;

public class Handler(ISourceRepository sourceRepository) : IQueryHandler<Query, Result>
{

    private readonly ISourceRepository _sourceRepository = sourceRepository;

    public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
    {
        var sources = await _sourceRepository.GetActiveWithMetaAsync(cancellationToken);
        var sourcesDto = sources.Select(s => new SourceDto(s.Id, s.Name.Value, new TimeframeDto(s.Timeframe.StartYear.Value,
                                                                                                s.Timeframe.EndYear.Value,
                                                                                                s.Timeframe.StartMonth.Value,
                                                                                                s.Timeframe.EndMonth.Value))).ToList();
        return new Result(new MetaDto(sourcesDto));
    }
}
