using Featurize.AspNetCore;
using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Admin.Features.Subscriptions;

public class SubscriptionsFeature :
    IUseFeature,
    IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<SubscriptionId>();
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/v1/subscription")
            .WithTags("Subscription");

        group.MapGetSubscriptions();
        group.MapCreateSubscription();
        group.MapListSubscriptions();
    }
}
