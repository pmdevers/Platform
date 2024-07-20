namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public record RestSchuld(Amount Bruto, Amount Aflossing, Amount Netto)
{
    public static RestSchuld Create(Leningdeel leningdeel, int termijn)
    {
        if (termijn < 1)
        {
            return new(leningdeel.Hoofdsom, 0, leningdeel.Hoofdsom);
        }

        if (termijn > leningdeel.Looptijd)
        {
            termijn = leningdeel.Looptijd;
        }

        var termijnen = Termijnen.Create(leningdeel);
        var t = termijnen[..termijn];
        var sum_aflossingen = t.Sum(x => (decimal)x.Betaling);
        var sum_rente = t.Sum(x => (decimal)x.Rente);
        var hoofdsom = t[0].BeginStand + sum_rente;
        return new RestSchuld(hoofdsom, sum_aflossingen, t[^1].Eindstand);
    }

    public static RestSchuld OpDatum(Leningdeel leningdeel, DateOnly datum)
        => Create(leningdeel,
            ((datum.Year - leningdeel.StartDatum.Year) * 12) +
            datum.Month - leningdeel.StartDatum.Month);

    public static RestSchuld EindeTermijn(Leningdeel leningdeel, int termijn)
       => Create(leningdeel, termijn);

    public static RestSchuld EindeLooptijd(Leningdeel leningdeel)
        => Create(leningdeel, leningdeel.Looptijd);

    public static RestSchuld EindeRenteVastePeriode(Leningdeel leningdeel)
        => Create(leningdeel, leningdeel.RenteVastePeriode.Looptijd);
};
