using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Source;

namespace CurrencyRates.Microservices.Rates.Domain.Aggregates;

public class Source : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Status Status { get; private set; }
    public SyncStrategy SyncStrategy { get; private set; }
    public CronExpression CronExpression { get; private set; }

    private Source()
    {

    }

    public static Source Create(Guid id, Name name, Status status, SyncStrategy syncStrategy, CronExpression cronExpression)
        => new()
        {
            Id = id,
            Name = name,
            Status = status,
            SyncStrategy = syncStrategy,
            CronExpression = cronExpression
        };

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
