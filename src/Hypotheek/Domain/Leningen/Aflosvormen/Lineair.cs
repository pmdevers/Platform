namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Aflosvormen;

public sealed class Lineair : IAflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public string Naam => nameof(Lineair);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return leningdeel.Hoofdsom / leningdeel.Looptijd;
    }

    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
        => 0;
}
