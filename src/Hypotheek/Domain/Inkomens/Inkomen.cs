using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Inkomens;

public class Inkomen : IAggregateState<LeningId>
{
    public LeningId Id { get; set; }
}
