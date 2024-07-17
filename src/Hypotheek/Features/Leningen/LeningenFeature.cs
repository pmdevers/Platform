using Featurize.AspNetCore;
using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public class LeningenFeature : IUseFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<LeningId>();
        options.AddAggregate<LeningdeelId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/leningen")
            .WithTags("Leningen");

        group.MapGet("/", () => "Hello World!");
    }
}
