using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Aanvragers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Aanvragers;

public static class CreateRechtspersoon
{
    public static void MapCreateRechtspersoon(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/rechtspersoon", HandleAsyc);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsyc(
        [AsParameters] AanvragersServices services,
        [FromBody] CreateRechtspersoonRequest request
        )
    {
        if (request.AanvragerId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var persoon = Rechtspersoon.Create(request.AanvragerId);

        await services.RechtspersoonManager.SaveAsync(persoon);

        return TypedResults.Ok();
    }

    public record CreateRechtspersoonRequest(AanvragerId AanvragerId);
}


public static class CreateNatuurlijkPersoon
{
    public static void MapCreateNatuurlijkPersoon(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/natuurlijk-persoon", HandleAsyc);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsyc(
        [AsParameters] AanvragersServices services,
        [FromBody] CreateNatuurlijkPersoonRequest request
        )
    {
        if (request.AanvragerId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var persoon = NatuurlijkPersoon.Create(request.AanvragerId);

        await services.NatuurlijkPersoonManager.SaveAsync(persoon);

        return TypedResults.Ok();
    }

    public record CreateNatuurlijkPersoonRequest(AanvragerId AanvragerId);
}
