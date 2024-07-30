
using FinSecure.Platform.Hypotheek.Domain.Onderpanden.Events;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Onderpanden;

public class Onderpand : IAggregateState<OnderpandId>
{
    public OnderpandId Id { get; set; } = OnderpandId.Next();
    public OnderpandType Type { get; set; } = OnderpandType.Onbekend;
    
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


