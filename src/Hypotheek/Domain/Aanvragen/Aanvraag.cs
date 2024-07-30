
using FinSecure.Platform.Hypotheek.Domain.Aanvragen.Events;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragen;

public class Aanvraag : IAggregateState<AanvraagId>
{
    public AanvraagId Id { get; set; }
    public List<Aanvrager> Aanvragers { get; set; } = [];
    public LeningId LeningId { get; set; } = LeningId.Empty;
    public OnderpandId OnderpandId { get; set; } = OnderpandId.Empty;
    public string Opmerking { get; set; } = string.Empty;
    
}
