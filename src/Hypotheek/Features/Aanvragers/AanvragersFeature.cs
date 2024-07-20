using Featurize.AspNetCore;
using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragers;

public class AanvragersFeature : IUseFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<AanvragerId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/aanvragers/")
            .WithTags("Aanvragers");

        group.MapCreateNatuurlijkPersoon();
        group.MapCreateRechtspersoon();
    }
}
