using Featurize.DomainModel;

namespace FinSecure.Platform.Admin.Domain.Subscriptions.Events;

public record SubscriptionCreated(string Name) : EventRecord;
