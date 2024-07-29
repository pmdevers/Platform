using Featurize.AspNetCore;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public class LeningenFeature : IUseFeature
{ 
    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/leningen")
            .WithTags("Leningen");

        group.MapCreateLening();
        group.MapGetLening();
        group.MapAddLeningdeel();
        group.MapDeleteLeningdeel();
        group.MapGetLeningdeelTermijnen();
    }
}
