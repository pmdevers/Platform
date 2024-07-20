namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Events;


public record LineairLeningdeelToegevoegd(
    LeningId LeningId,
    LeningdeelId LeningdeelId,
    DateOnly StartDatum,
    int Looptijd,
    RenteVastePeriode RenteVastePeriode,
    Amount Hoofdsom
    ) : EventRecord;