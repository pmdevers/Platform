
namespace FinSecure.Platform.Hypotheek.Domain.Leningen;

public class Lening : AggregateRoot<LeningId>
{
    private Lening(InkomenId id) : base(id)
    {
    }

    public static Lening Create()
    {
        var lening = new Lening(LeningId.Next());
        return lening;
    }
}
