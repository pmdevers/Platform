namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen.Events;

public record AanvraagCreated(AanvraagId AanvraagId, string Opmerking) : EventRecord;
