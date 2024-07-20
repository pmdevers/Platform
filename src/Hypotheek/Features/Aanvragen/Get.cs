
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public static class Get
{
    public static void MapGetAanvraag(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("{aanvraagId}", HandleAsync);
    }

    private static async Task<Results<Ok<Aanvraag>, BadRequest, NotFound>> HandleAsync(
        [AsParameters] AanvragenServices services,
        [FromRoute] AanvraagId aanvraagId
        )
    {
        if (aanvraagId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var aanvraag = await services.Manager.LoadAsync(aanvraagId);

        if(aanvraag is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(aanvraag);
    }
}
