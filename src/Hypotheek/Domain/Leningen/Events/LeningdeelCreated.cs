namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Events;

public record LeningdeelCreated(
    LeningdeelId LeningdeelId,
    DateOnly IngangsDatum,
    int Looptijd,
    RenteVastePeriode RenteVastePeriode,
    decimal Hoofdsom
    ) : EventRecord;
