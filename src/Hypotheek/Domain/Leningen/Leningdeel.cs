
using FinSecure.Platform.Hypotheek.Domain.Leningen.Aflosvormen;
using FinSecure.Platform.Hypotheek.Domain.Leningen.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public class Leningdeel : AggregateRoot<LeningdeelId>
{
    public IAflosvorm Aflostvorm { get; private set; } 
        = new Annuitair();
    public DateOnly IngangsDatum { get; private set; } 
    public int Looptijd { get; private set; }
    public RenteVastePeriode RenteVastePeriode { get; private set; } 
        = RenteVastePeriode.Create(5m, 120);
    public decimal Hoofdsom { get; private set; }
    public DateOnly EindDatum { get; private set; }

    private Leningdeel(LeningdeelId id) : base(id)
    {
    }

    public Amount GetAflossing(Amount rente, int termijn)
        => Aflostvorm.GetAflossing(this, rente, termijn);

    public Amount GetKapitaal(Amount rente, int termijn)
        => Aflostvorm.GetKapitaal(this, rente, termijn);

    public static Leningdeel Create(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom)
    {
        var leningdeel = new Leningdeel(LeningdeelId.Next());
        leningdeel.RecordEvent(new LeningdeelCreated(leningdeel.Id, startDatum, looptijd, renteVastePeriode, hoofdsom));
        return leningdeel;
    }

    internal void Apply(LeningdeelCreated e)
    {
        IngangsDatum = e.IngangsDatum;
        Looptijd = e.Looptijd;
        RenteVastePeriode = e.RenteVastePeriode;
        Hoofdsom = e.Hoofdsom;
        EindDatum = IngangsDatum.AddMonths(e.Looptijd);
    }
}

public interface IAflosvorm
{
    Percentage Boetevrij { get; }
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
    Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn);
}