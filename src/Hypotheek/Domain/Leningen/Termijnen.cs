using static FinSecure.Platform.Hypotheek.Domain.Leningen.Termijnen;

namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public class Termijnen : List<Termijn>
{
    private readonly Leningdeel _leningdeel;

    private Termijnen(Leningdeel leningdeel)
    {
        _leningdeel = leningdeel;
    }

    public static Termijnen Create(Leningdeel leningdeel)
    {
        var termijn = new Termijnen(leningdeel);
        termijn.Genereer();
        return termijn;
    }

    private void Genereer()
    {
        var resterend = _leningdeel.Hoofdsom;

        for (int i = 1; i <= _leningdeel.Looptijd; i++)
        {
            var rente = resterend * _leningdeel.RenteVastePeriode.MaandRente;
            var aflossing = _leningdeel.GetAflossing(rente, i);
            var eindstand = Amount.Create(Math.Max((decimal)resterend - (decimal)aflossing, 0));
            var betaling = aflossing + rente;

            Add(new Termijn(resterend, rente, aflossing, betaling, eindstand));

            resterend = eindstand;
        }
    }
    public sealed record Termijn(Amount BeginStand, Amount Rente, Amount Aflossing, Amount Betaling, Amount Eindstand);
}
