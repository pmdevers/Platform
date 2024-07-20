
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Leningen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public static class CreateLening
{
    public static void MapCreateLening(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", HandleAsync);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsync(
        [AsParameters] LeningenServices services,
        [FromBody] CreateLeningRequest request
        )
    {
        if(request.LeningId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var lening = Lening.Create(request.LeningId);

        await services.Manager.SaveAsync(lening);

        return TypedResults.Ok();
    }

    public record CreateLeningRequest(LeningId LeningId);
}
