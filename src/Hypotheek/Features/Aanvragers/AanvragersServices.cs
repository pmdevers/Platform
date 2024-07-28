using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragers;

public class AanvragersServices(
    IAggregateManager<NatuurlijkPersoon, AanvragerId> natuurlijkPersoonManager,
    IAggregateManager<Rechtspersoon, AanvragerId> rechtspersoonManager
    )
{
    public IAggregateManager<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoonManager { get; } = natuurlijkPersoonManager;
    public IAggregateManager<Rechtspersoon, AanvragerId> RechtspersoonManager { get; } = rechtspersoonManager;
}
