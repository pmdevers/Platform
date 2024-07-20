namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Aflosvormen;

public sealed class Aflossingsvrij(Amount MaandelijkseAflossing) : IAflosvorm
{
    public string Naam => nameof(Aflossingsvrij);

    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return MaandelijkseAflossing;
    }
    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
        => 0;
}