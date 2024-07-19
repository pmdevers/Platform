using FinSecure.Platform.Hypotheek.Domain.Aanvragers.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragers;

public class Rechtspersoon : AggregateRoot<AanvragerId>
{
    private Rechtspersoon(AanvragerId id) : base(id)
    {

    }

    public static Rechtspersoon Create(AanvragerId id)
    {
        var persoon = new Rechtspersoon(id);
        persoon.RecordEvent(new RechtspersoonCreated(id));
        return persoon;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S1186:Methods should not be empty", Justification = "<Pending>")]
    internal void Apply(RechtspersoonCreated e)
    {

    }
}
