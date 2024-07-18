using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Leningen;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public class LeningenServices(AggregateManager<Lening, LeningId> manager)
{
    public AggregateManager<Lening, InkomenId> Manager { get; } = manager;
}
