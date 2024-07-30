
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StreamWave;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public static class AddAanvrager
{
    public static void MapAddAanvrager(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("{aanvraagId}/aanvragers", HandleAsync);
    }

    private static async Task<Results<Ok, BadRequest, NotFound>> HandleAsync(
        [AsParameters] AanvragenServices services,
        [FromRoute] AanvraagId aanvraagId,
        [FromBody] AddAanvragerRequest request
        )
    {
        if (aanvraagId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        await services.Manager.LoadAsync(aanvraagId);

        await services.NatuurlijkPersoonManager.LoadAsync(request.AanvragerId);
               

        return TypedResults.Ok();
    }

    public record AddAanvragerRequest(AanvragerType Type, AanvragerId AanvragerId);
}
