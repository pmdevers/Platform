using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenServices(
    IAggregate<Aanvraag, AanvraagId> manager,
    IAggregate<NatuurlijkPersoon, AanvragerId> natuurlijkPersoonManager,
    IAggregate<Rechtspersoon, AanvragerId> rechtspersoonManager
    )
{
    public IAggregate<Aanvraag, AanvraagId> Manager { get; } = manager;
    public IAggregate<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoonManager { get; } = natuurlijkPersoonManager;
    public IAggregate<Rechtspersoon, AanvragerId> RechtspersoonManager { get; } = rechtspersoonManager;
}