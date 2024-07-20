using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenServices(
    AggregateManager<Aanvraag, AanvraagId> manager,
    AggregateManager<NatuurlijkPersoon, AanvragerId> natuurlijkPersoonManager,
    AggregateManager<Rechtspersoon, AanvragerId> rechtspersoonManager
    )
{
    public AggregateManager<Aanvraag, AanvraagId> Manager { get; } = manager;
    public AggregateManager<NatuurlijkPersoon, AanvragerId> NatuurlijkPersoonManager { get; } = natuurlijkPersoonManager;
    public AggregateManager<Rechtspersoon, AanvragerId> RechtspersoonManager { get; } = rechtspersoonManager;
}