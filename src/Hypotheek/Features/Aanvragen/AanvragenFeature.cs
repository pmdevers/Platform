using Featurize.AspNetCore;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddAggregate<Aanvraag, AanvraagId>(() => new Aanvraag() { Id = AanvraagId.Next() });
        services.AddAggregate<Rechtspersoon, AanvragerId>(() => new Rechtspersoon() { Id = AanvragerId.Next() });
        services.AddAggregate<NatuurlijkPersoon, AanvragerId>(() => new NatuurlijkPersoon() { Id = AanvragerId.Next() });
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/aanvragen")
            .WithTags("Aanvragen");

        group.MapGetAanvraag();
        group.MapCreateAanvraag();
        group.MapAddAanvrager();
    }
}
