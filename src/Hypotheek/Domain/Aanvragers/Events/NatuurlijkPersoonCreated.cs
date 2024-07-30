using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragers.Events;

public record NatuurlijkPersoonCreated(AanvragerId AanvragerId) : Event;
