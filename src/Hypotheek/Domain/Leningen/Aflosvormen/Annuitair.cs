namespace FinSecure.Platform.Hypotheek.Domain.Leningen.Aflosvormen;

public sealed class Annuitair : IAflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        var hypotheek = (double)leningdeel.Hoofdsom;
        var maandrente = (double)leningdeel.RenteVastePeriode.MaandRente;

        var annuiteit = maandrente / (1 - Math.Pow(1 + maandrente, -leningdeel.Looptijd)) * hypotheek;

        return (decimal)annuiteit - rente;
    }
    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
        => 0;
}
