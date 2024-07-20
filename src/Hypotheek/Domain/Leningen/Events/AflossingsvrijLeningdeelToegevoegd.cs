namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Events;

public record AflossingsvrijLeningdeelToegevoegd(
        LeningId LeningId,
        LeningdeelId LeningdeelId,
        DateOnly StartDatum,
        int Looptijd,
        RenteVastePeriode RenteVastePeriode,
        Amount Hoofdsom,
        Amount ExtraMaandelijkseAflossing
    ) : EventRecord;