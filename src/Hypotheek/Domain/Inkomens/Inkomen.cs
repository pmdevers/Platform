namespace FinSecure.Platform.Hypotheek.Domain.Inkomens;

public class Inkomen : AggregateRoot<InkomenId>
{
    private Inkomen(InkomenId id) : base(id)
    {
    }

    public static Inkomen Create()
    {
        var inkomen = new Inkomen(InkomenId.Next());
        return inkomen;
    }
}
