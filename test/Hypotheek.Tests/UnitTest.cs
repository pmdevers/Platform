using FinSecure.Platform.Hypotheek.Domain.Aanvragen;

namespace Hypotheek.Tests;

public class UnitTest
{
    [Fact]
    public void Test()
    {
        var aanvraag = Aanvraag.Create(AanvraagId.Next());

        var events = aanvraag.GetUncommittedEvents();

        Assert.Single(events);
    }
}
