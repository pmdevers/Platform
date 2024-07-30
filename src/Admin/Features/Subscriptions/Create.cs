
using FinSecure.Platform.Common.Storage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FinSecure.Platform.Admin.Domain.Subscriptions;

namespace FinSecure.Platform.Admin.Features.Subscriptions;

public static class Create
{
    public static void MapCreateSubscription(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", HandleAsync);
    }

    private static async Task<Results<Ok<SubscriptionId>, BadRequest>> HandleAsync(
        [FromBody] CreateSubscriptionRequest request
        )
    {
        var sub = Subscription.Create(request.Name);
        await Task.CompletedTask;
        return TypedResults.Ok(sub.Id);
    }

    public record CreateSubscriptionRequest(string Name);

}
