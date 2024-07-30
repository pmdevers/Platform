using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Leningen;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public class LeningenServices(IAggregateManager<Lening, LeningId> manager)
{
    public IAggregateManager<Lening, LeningId> Manager { get; } = manager;
}
