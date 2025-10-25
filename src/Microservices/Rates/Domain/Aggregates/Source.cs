using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.ValueObjects.Source;

namespace MarketHub.Microservices.Rates.Domain.Aggregates;

public class Source : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Status Status { get; private set; }
    public SyncStrategy SyncStrategy { get; private set; }
    public Cron Cron { get; private set; }

    public ICollection<Table> Tables { get; private set; } = [];

    private Source() { }

    public Source(Guid id, Name name, Status status, SyncStrategy syncStrategy, Cron cron)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Name = name;
        Status = status;
        SyncStrategy = syncStrategy;
        Cron = cron;
    }

    public static Source Create(Guid id, Name name, Status status, SyncStrategy syncStrategy, Cron cron)
        => new(id, name, status, syncStrategy, cron);

    public static Source Create(Name name, Status status, SyncStrategy syncStrategy, Cron cron)
        => new(Guid.Empty, name, status, syncStrategy, cron);

    public void Inactivate()
        => ChangeStatusTo(Status.Inactive);

    public void Activate()
        => ChangeStatusTo(Status.Active);

    private void ChangeStatusTo(Status newStatus)
    {
        if (!Status.CanTransitionTo(newStatus))
            throw new InvalidOperationException($"Cannot change status of source from {Status.Name} to {newStatus.Name}");
        Status = newStatus;
    }
}
