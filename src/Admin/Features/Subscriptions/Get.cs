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

    private static Ok HandleAsync()
    {
       return TypedResults.Ok();
    }
}
