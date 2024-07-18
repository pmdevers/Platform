namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Events;

public record AnnuitairLeningdeelToegevoegd(
    LeningId LeningId,
    LeningdeelId LeningdeelId,
    DateOnly StartDatum,
    int Looptijd,
    RenteVastePeriode RenteVastePeriode,
    Amount Hoofdsom
    ) : EventRecord;
