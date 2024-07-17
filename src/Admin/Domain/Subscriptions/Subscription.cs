using Featurize.DomainModel;
using FinSecure.Platform.Admin.Domain.Subscriptions.Events;
using FinSecure.Platform.Common.ValueObjects;

namespace FinSecure.Platform.Admin.Domain.Subscriptions;

public class Subscription : AggregateRoot<SubscriptionId>
{
    public string Name { get; private set; } = string.Empty;

    private Subscription(SubscriptionId id) : base(id)
    {
    }

    public static Subscription Create(string name)
    {
        var subscription = new Subscription(SubscriptionId.Next());
        subscription.RecordEvent(new SubscriptionCreated(name));
        return subscription;
    }

    internal void Apply(SubscriptionCreated e)
    {
        Name = e.Name;
    }
}
