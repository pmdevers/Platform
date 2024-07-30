using StreamWave;

namespace FinSecure.Platform.Admin.Domain.Subscriptions.Events;

public record SubscriptionCreated(string Name) : Event;
