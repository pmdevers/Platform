using FinSecure.Platform.Hypotheek.Domain.Aanvragers.Events;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragers;

public class Rechtspersoon : IAggregateState<AanvragerId>
{
    public AanvragerId Id { get; set; }
}
