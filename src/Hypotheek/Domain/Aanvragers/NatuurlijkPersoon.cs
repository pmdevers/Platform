using FinSecure.Platform.Hypotheek.Domain.Aanvragers.Events;

namespace FinSecure.Platform.Hypotheek.Domain.Aanvragers;

public class NatuurlijkPersoon : AggregateRoot<AanvragerId>
{
    private NatuurlijkPersoon(AanvragerId id) : base(id)
    {
    }

    public static NatuurlijkPersoon Create(AanvragerId id)
    {
        var persoon = new NatuurlijkPersoon(id);
        persoon.RecordEvent(new NatuurlijkPersoonCreated(id));
        return persoon;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S1186:Methods should not be empty", Justification = "<Pending>")]
    internal void Apply(NatuurlijkPersoonCreated e)
    {

    }
}

