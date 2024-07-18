
using FinSecure.Platform.Hypotheek.Domain.Onderpanden.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Onderpanden;

public class Onderpand : AggregateRoot<OnderpandId>
{
    public OnderpandType Type { get; private set; } = OnderpandType.Onbekend;

    private Onderpand(OnderpandId id) : base(id)
    {
    }

    public static Onderpand Create(OnderpandId onderpandId)
    {
        var onderpand = new Onderpand(onderpandId);
        onderpand.RecordEvent(new OnderpandCreated(onderpandId, OnderpandType.Onbekend));
        return onderpand;
    }

    internal void Apply(OnderpandCreated e)
    {
        Type = e.OnderpandType;
    }
}

public abstract record OnderpandType
{
    public static OnderpandType Onbekend => new Onbekend();

    public abstract Amount GetWaarde();

}

public record Onbekend : OnderpandType
{
    public override Amount GetWaarde()
        => Amount.Zero;
}


