using Featurize.AspNetCore;
using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public class AanvragenFeature : IUseFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<AanvraagId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/aanvragen")
            .WithTags("Aanvragen");

        group.MapCreateAanvraag();
    }
}
