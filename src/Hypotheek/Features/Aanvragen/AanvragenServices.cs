using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenServices(AggregateManager<Aanvraag, AanvraagId> manager)
{
    public AggregateManager<Aanvraag, AanvraagId> Manager { get; } = manager;
}