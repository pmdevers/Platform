using FinSecure.Platform.Hypotheek.Domain.Leningen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public static class GetLening
{
    public static void MapGetLening(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{leningId}", HandleAsync);
    }

    private static async Task<Results<Ok<Lening>, NotFound>> HandleAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId
        )
    {
        var lening = await services.Manager.LoadAsync(leningId);

        if(lening is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(lening);
    }
}
