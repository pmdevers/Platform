
using FinSecure.Platform.Hypotheek.Domain.Aanvragen.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen;

public class Aanvraag : AggregateRoot<AanvraagId>
{
    public LeningId LeningId { get; private set; } = LeningId.Empty;
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

    internal void Apply(AanvraagCreated e)
    {
        Opmerking = e.Opmerking;
        LeningId = LeningId.Unknown;
    }
}
