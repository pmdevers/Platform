using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragers;

public class AanvragersServices(
    IAggregate<NatuurlijkPersoon, AanvragerId> natuurlijkPersoon,
    IAggregate<Rechtspersoon, AanvragerId> rechtspersoon
    )
{
    public IAggregate<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoon { get; } = natuurlijkPersoon;
    public IAggregate<Rechtspersoon, AanvragerId> Rechtspersoon { get; } = rechtspersoon;
}
