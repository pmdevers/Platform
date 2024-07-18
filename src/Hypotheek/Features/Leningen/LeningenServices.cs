using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Hypotheek.Domain.Leningen;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public class LeningenServices(AggregateManager<Lening, LeningId> leningManager, AggregateManager<Leningdeel, LeningdeelId> leningdeelManager)
{
    public AggregateManager<Lening, InkomenId> LeningManager { get; } = leningManager;
    public AggregateManager<Leningdeel, InkomenId> LeningdeelManager { get; } = leningdeelManager;
}
