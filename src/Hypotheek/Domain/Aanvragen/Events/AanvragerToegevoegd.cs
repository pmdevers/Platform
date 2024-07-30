using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen.Events;

public record AanvragerToegevoegd(AanvraagId AanvraagId, AanvragerType AanvragerType, AanvragerId AanvragerId) : Event;