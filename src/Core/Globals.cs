global using ProjectId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Core.ForProject>;
global using SubscriptionId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Core.ForSubscription>;


namespace FinSecure.Platform.Core;

#pragma warning disable S2094 // Classes should not be empty

public class ForProject : GuidBehaviour;
public class ForSubscription : GuidBehaviour;

#pragma warning restore S2094 // Classes should not be empty