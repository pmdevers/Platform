global using AanvraagId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForAanvraag>;
global using LeningId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForLening>;
global using LeningdeelId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForLeningdeel>;
global using InkomenId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForInkomen>;
global using OnderpandId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForOnderpand>;
global using AanvragerId = Featurize.ValueObjects.Identifiers.Id<FinSecure.Platform.Hypotheek.ForAanvrager>;


namespace FinSecure.Platform.Hypotheek;
#pragma warning disable S2094 // Classes should not be empty

public class ForAanvraag : GuidBehaviour;
public class ForLening : GuidBehaviour;
public class ForLeningdeel : GuidBehaviour;
public class ForInkomen : GuidBehaviour;
public class ForOnderpand : GuidBehaviour;

public class ForAanvrager : GuidBehaviour;

#pragma warning restore S2094 // Classes should not be empty