using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragen;

public static class CreateAanvraag
{
    public static void MapCreateAanvraag(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", HandleAsync);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsync(
        [AsParameters] AanvragenServices services,
        [FromBody] CreateAanvraagRequest request
        )
    {
        if (request.AanvraagId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var aanvraag = Aanvraag.Create(request.AanvraagId);

        await services.Manager.SaveAsync(aanvraag);

        return TypedResults.Ok();
    }

    public record CreateAanvraagRequest(AanvraagId AanvraagId);
}
