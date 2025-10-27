using System;
using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.ValueObjects.Source;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;

public static class SourceSeeder
{
    public static Source NbpApiDateRangeSource
        => Source.Create(Guid.Parse("d07ebbb0-ee4b-4d13-8ef7-8ef007ae77e3"),
                         Name.FromValue("Kursy średnie walut obcych – tabela B"),
                         Status.Active,
                         SyncStrategy.NbpApiDateRange,
                         Cron.FromValue("* * 16 3 * *"));

}
