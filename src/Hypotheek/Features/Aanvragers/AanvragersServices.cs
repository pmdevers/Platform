using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragers;

public class AanvragersServices(
    AggregateManager<NatuurlijkPersoon, AanvragerId> natuurlijkPersoonManager,
    AggregateManager<Rechtspersoon, AanvragerId> rechtspersoonManager
    )
{
    public AggregateManager<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoonManager { get; } = natuurlijkPersoonManager;
    public AggregateManager<Rechtspersoon, AanvragerId> RechtspersoonManager { get; } = rechtspersoonManager;
}
