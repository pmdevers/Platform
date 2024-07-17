namespace FinSecure.Platform.Admin.Domain.Subscriptions;

public record AccountSubscription(Guid AccountId, SubscriptionId SubscriptionId, string SubscriptionName);
