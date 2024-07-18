namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public static class BoeteRente
{
    public static Amount Oversluiten(Leningdeel leningdeel, RenteVastePeriode renteVastePeriode, DateOnly ingangsDatum)
    {
        var einddatum = leningdeel.StartDatum.AddMonths(leningdeel.RenteVastePeriode.Looptijd);

        var diff = (leningdeel.RenteVastePeriode.Rente - renteVastePeriode.Rente) / 12;
        var maanden = ((einddatum.Year - ingangsDatum.Year) * 12)
            + einddatum.Month - ingangsDatum.Month;

        var restschuld = RestSchuld.OpDatum(leningdeel, leningdeel.StartDatum);

        var boetevrij = restschuld.Netto * leningdeel.AflostVorm.Boetevrij;

        return ((restschuld.Netto - boetevrij) * diff) * maanden;
    }
}