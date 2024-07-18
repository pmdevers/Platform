
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public static class DeleteLeningdeel
{
    public static void MapDeleteLeningdeel(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/{leningId}/leningdeel", HandleAsync);
    }

    private static async Task<Results<Ok, BadRequest, NotFound>> HandleAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId,
        [FromBody] DeleteLeningdeelRequest request
        )
    {
        if(leningId.IsEmpty() || leningId == LeningId.Unknown)
        {
            return TypedResults.BadRequest();
        }

        var lening = await services.Manager.LoadAsync(leningId);

        if(lening is null)
        {
            return TypedResults.NotFound();
        }

        lening.DeleteLeningdeel(request.LeningdeelId);

        await services.Manager.SaveAsync(lening);

        return TypedResults.Ok();
    }

    public record DeleteLeningdeelRequest(LeningdeelId LeningdeelId);
}
