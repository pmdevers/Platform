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

        await services.Rechtspersoon.LoadAsync(request.AanvragerId);

        await services.Rechtspersoon.SaveAsync();

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

        await services.NatuurlijkPersoon.LoadAsync(request.AanvragerId);
        await services.NatuurlijkPersoon.SaveAsync();

        return TypedResults.Ok();
    }

    public record CreateNatuurlijkPersoonRequest(AanvragerId AanvragerId);
}
