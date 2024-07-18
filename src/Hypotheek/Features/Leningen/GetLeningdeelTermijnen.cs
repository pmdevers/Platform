
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Leningen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public static class GetLeningdeelTermijnen
{
    public static void MapGetLeningdeelTermijnen(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{leningId}/termijnen/{leningdeelId}", HandleAsync);
    }

    private static async Task<Results<Ok<GetTermijnenResponse>, NotFound, BadRequest>> HandleAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId,
        [FromRoute] LeningdeelId leningdeelId
        )
    {
        if (leningId.IsEmptyOrUnknown() || leningdeelId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var lening = await services.Manager.LoadAsync(leningId);

        var leningdeel = lening?.Leningdelen.FirstOrDefault(x => x.LeningdeelId == leningdeelId);

        if (lening is null || leningdeel is null)
        {
            return TypedResults.NotFound();
        }

        var termijnen = Termijnen.Create(leningdeel)
            .Select((item, index) => new TermijnResponse(
                index + 1, 
                item.BeginStand + Currency.Euro,
                item.Rente + Currency.Euro,
                item.Aflossing + Currency.Euro,
                item.Betaling + Currency.Euro,
                item.Eindstand + Currency.Euro));

        return TypedResults.Ok(new GetTermijnenResponse(termijnen));

    }

    public record GetTermijnenResponse(IEnumerable<TermijnResponse> Termijnen);

    public record TermijnResponse(int Termijn, Money Beginstand, Money Rente, Money Aflossing, Money Betaling, Money Eindstand);
}
