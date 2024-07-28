using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenServices(
    IAggregateManager<Aanvraag, AanvraagId> manager,
    IAggregateManager<NatuurlijkPersoon, AanvragerId> natuurlijkPersoonManager,
    IAggregateManager<Rechtspersoon, AanvragerId> rechtspersoonManager
    )
{
    public IAggregateManager<Aanvraag, AanvraagId> Manager { get; } = manager;
    public IAggregateManager<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoonManager { get; } = natuurlijkPersoonManager;
    public IAggregateManager<Rechtspersoon, AanvragerId> RechtspersoonManager { get; } = rechtspersoonManager;
}