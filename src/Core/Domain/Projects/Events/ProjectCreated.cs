using Featurize.DomainModel;

namespace FinSecure.Platform.Core.Domain.Projects.Events;

public record ProjectCreated(ProjectId ProjectId, SubscriptionId SubscriptionId, string Name) : EventRecord;
