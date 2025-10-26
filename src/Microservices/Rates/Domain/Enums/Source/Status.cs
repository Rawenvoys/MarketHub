using Ardalis.SmartEnum;
namespace MarketHub.Microservices.Rates.Domain.Enums.Source;

public abstract class Status : SmartEnum<Status>
{
    public static readonly Status Inactive = new InactiveStatus(0, nameof(Inactive));
    public static readonly Status Active = new ActiveStatus(1, nameof(Active));

    public abstract bool CanTransitionTo(Status next);

    private Status(int value, string name) : base(name, value) { }

    private sealed class InactiveStatus(int value, string name) : Status(value, name)
    {
        public override bool CanTransitionTo(Status next)
            => next.Equals(Active);
    }

    private sealed class ActiveStatus(int value, string name) : Status(value, name)
    {
        public override bool CanTransitionTo(Status next)
            => next.Equals(Inactive);
    }
}

