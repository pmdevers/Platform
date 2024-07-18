
using FinSecure.Platform.Hypotheek.Domain.Leningen.Aflosvormen;
using FinSecure.Platform.Hypotheek.Domain.Leningen.Events;
using System.Diagnostics.CodeAnalysis;

namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public class Lening : AggregateRoot<LeningId>
{
    private readonly List<Leningdeel> _leningdelen = [];

    public IReadOnlyList<Leningdeel> Leningdelen => _leningdelen.AsReadOnly();

    private Lening(LeningId id) : base(id)
    {
    }

    public static Lening Create(LeningId leningId)
    {
        var lening = new Lening(leningId);
        lening.RecordEvent(new LeningCreated(leningId));
        return lening;
    }
    
    public void AddAnnuitair(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
    {
        RecordEvent(new AnnuitairLeningdeelToegevoegd(Id, LeningdeelId.Next(), startDatum, looptijd, renteVastePeriode, hoofdsom));
    }

    public void AddAflossingsvrij(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom, Amount extraMaandelijkseAflossing)
    {
        RecordEvent(new AflossingsvrijLeningdeelToegevoegd(Id, LeningdeelId.Next(), startDatum, looptijd, renteVastePeriode, hoofdsom, extraMaandelijkseAflossing));
    }

    public void AddLineair(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
    {
        RecordEvent(new LineairLeningdeelToegevoegd(Id, LeningdeelId.Next(), startDatum, looptijd, renteVastePeriode, hoofdsom));
    }

    internal void DeleteLeningdeel(LeningdeelId leningdeelId)
    {
        RecordEvent(new LeningdeelVerwijdert(Id, leningdeelId));
    }


    [SuppressMessage("Style", "IDE0060:Remove unused parameter", 
        Justification = "Parameter is required by the api.")]
    internal void Apply(LeningCreated e)
    {
        _leningdelen.Clear();
    }

    internal void Apply(AnnuitairLeningdeelToegevoegd e)
    {
        var leningdeel = new Leningdeel(e.LeningdeelId, new Annuitair(), e.StartDatum, e.Looptijd, e.RenteVastePeriode, e.Hoofdsom);
        _leningdelen.Add(leningdeel);
    }

    internal void Apply(LineairLeningdeelToegevoegd e)
    {
        var leningdeel = new Leningdeel(e.LeningdeelId, new Lineair(), e.StartDatum, e.Looptijd, e.RenteVastePeriode, e.Hoofdsom);
        _leningdelen.Add(leningdeel);
    }

    internal void Apply(AflossingsvrijLeningdeelToegevoegd e)
    {
        var leningdeel = new Leningdeel(e.LeningdeelId, new Aflossingsvrij(e.ExtraMaandelijkseAflossing), e.StartDatum, e.Looptijd, e.RenteVastePeriode, e.Hoofdsom);
        _leningdelen.Add(leningdeel);
    }

    internal void Apply(LeningdeelVerwijdert e)
    {
        _leningdelen.RemoveAll(x => x.LeningdeelId == e.LeningdeelId);
    }
}


