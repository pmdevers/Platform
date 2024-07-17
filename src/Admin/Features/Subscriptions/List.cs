using FinSecure.Platform.Admin.Domain.Subscriptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Admin.Features.Subscriptions;

public static class List
{
    public static void MapListSubscriptions(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/list", HandleAsync);
    }

    private static async Task<Results<Ok<AccountSubscription[]>, BadRequest>> HandleAsync(
        [FromHeader(Name = "account-id")] Guid accountId,
        [FromServices] IEntityRepository<AccountSubscription, SubscriptionId> repository
        )
    {
        var result = await repository.Query
            .Where(x => x.AccountId == accountId)
            .ToArrayAsync();

        if (result.Length == 0)
        {
            return TypedResults.BadRequest();
        }

        return TypedResults.Ok(result);
    }
}
