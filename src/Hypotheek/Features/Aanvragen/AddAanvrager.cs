
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Aanvragen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        var aanvraag = await services.Manager.LoadAsync(aanvraagId);

        AggregateRoot<AanvragerId>? aanvrager = request.Type switch 
        { 
            AanvragerType.NatuurlijkPersoon => await services.NatuurlijkPersoonManager.LoadAsync(request.AanvragerId),
            AanvragerType.Rechtspersoon => await services.RechtspersoonManager.LoadAsync(request.AanvragerId),
            _ => throw new InvalidOperationException($"Type: '{request.Type}' aanvrager onbekend.")
        };

        if( aanvraag is null || aanvrager is null)
        {
            return TypedResults.NotFound();
        }

        aanvraag.AanvragerToevoegen(request.Type, aanvrager.Id);

        await services.Manager.SaveAsync(aanvraag);

        return TypedResults.Ok();
    }

    public record AddAanvragerRequest(AanvragerType Type, AanvragerId AanvragerId);
}
