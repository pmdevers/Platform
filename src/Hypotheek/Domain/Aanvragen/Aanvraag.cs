
using FinSecure.Platform.Hypotheek.Domain.Aanvragen.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen;

public class Aanvraag : AggregateRoot<AanvraagId>
{
    private readonly List<Aanvrager> _aanvragers = [];

    public IReadOnlyList<Aanvrager> Aanvragers => _aanvragers.AsReadOnly();
    public LeningId LeningId { get; private set; } = LeningId.Empty;
    public OnderpandId OnderpandId { get; private set; } = OnderpandId.Empty;
    public string Opmerking { get; private set; } = string.Empty;

    private Aanvraag(AanvraagId id) : base(id)
    {
    }

    public static Aanvraag Create(AanvraagId id, string opmerking = "")
    {
        var aanvraag = new Aanvraag(id);
        aanvraag.RecordEvent(new AanvraagCreated(id, opmerking));
        return aanvraag;
    }

    public void AanvragerToevoegen(AanvragerType aanvragerType, AanvragerId aanvragerId)
    {
        if(!_aanvragers.Exists(x => x.AanvragerId == aanvragerId))
        {
            RecordEvent(new AanvragerToegevoegd(Id, aanvragerType, aanvragerId));
        }
    }

    internal void Apply(AanvraagCreated e)
    {
        Opmerking = e.Opmerking;
        LeningId = LeningId.Unknown;
        OnderpandId = OnderpandId.Unknown;
    }

    internal void Apply(AanvragerToegevoegd e)
    {
        _aanvragers.Add(new Aanvrager(e.AanvragerType, e.AanvragerId));
    }
}
