using FinSecure.Platform.Common.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FinSecure.Platform.Admin.Domain.Subscriptions;

namespace FinSecure.Platform.Admin.Features.Subscriptions;

public static class Get
{
    public static void MapGetSubscriptions(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", HandleAsync);
    }

    private static async Task<Results<Ok<Subscription>, NotFound>> HandleAsync(
        [FromHeader(Name = "subscriptionId")] SubscriptionId subscriptionId,
        [FromServices] AggregateManager<Subscription, SubscriptionId> manager
        )
    {
        var sub = await manager.LoadAsync(subscriptionId);

        if (sub is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(sub);
    }
}
