namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public interface IAflosvorm
{
    string Naam { get; }
    Percentage Boetevrij { get; }
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
    Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn);
}

public record Leningdeel(LeningdeelId LeningdeelId, IAflosvorm AflostVorm, DateOnly StartDatum, int Looptijd, RenteVastePeriode RenteVastePeriode, Amount Hoofdsom)
{
    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);

    public Amount GetAflossing(Amount rente, int termijn)
        => AflostVorm.GetAflossing(this, rente, termijn);

    public Amount GetKapitaal(Amount rente, int termijn)
        => AflostVorm.GetKapitaal(this, rente, termijn);
}
