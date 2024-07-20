
using Featurize.ValueObjects;
using FinSecure.Platform.Hypotheek.Domain.Leningen;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Hypotheek.Features.Leningen;

public static class AddLeningdeel
{
    public static void MapAddLeningdeel(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/{leningId}/aflossingsvrij", HandleAflossingsvrijAsync);
        builder.MapPost("/{leningId}/annuitair", HandleAnnuitairAsync);
        builder.MapPost("/{leningId}/lineair", HandleLineairAsync);
    }

    private static async Task<Results<Ok, NotFound, BadRequest>> HandleAflossingsvrijAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId,
        [FromBody] CreateAflossingsvrijLeningdeelRequest request
        )
    {
        if (leningId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var lening = await services.Manager.LoadAsync(leningId);

        if (lening is null)
        {
            return TypedResults.NotFound();
        }

        lening.AddAflossingsvrij(request.StartDatum, request.Looptijd, request.RenteVastePeriode, request.Hoofdsom, request.ExtraMaandelijkseAflossing);

        await services.Manager.SaveAsync(lening);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, BadRequest>> HandleLineairAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId,
        [FromBody] CreateLeningdeelRequest request
        )
    {
        if (leningId.IsEmpty() || leningId == LeningId.Unknown)
        {
            return TypedResults.BadRequest();
        }

        var lening = await services.Manager.LoadAsync(leningId);

        if (lening is null)
        {
            return TypedResults.NotFound();
        }

        lening.AddLineair(request.StartDatum, request.Looptijd, request.RenteVastePeriode, request.Hoofdsom);

        await services.Manager.SaveAsync(lening);

        return TypedResults.Ok();
    }

    private static async Task<Results<Ok, NotFound, BadRequest>> HandleAnnuitairAsync(
        [AsParameters] LeningenServices services,
        [FromRoute] LeningId leningId,
        [FromBody] CreateLeningdeelRequest request
        )
    {
        if (leningId.IsEmpty() || leningId == LeningId.Unknown)
        {
            return TypedResults.BadRequest();
        }

        var lening = await services.Manager.LoadAsync(leningId);

        if (lening is null)
        {
            return TypedResults.NotFound();
        }

        lening.AddAnnuitair(request.StartDatum, request.Looptijd, request.RenteVastePeriode, request.Hoofdsom);

        await services.Manager.SaveAsync(lening);

        return TypedResults.Ok();
    }

    public record CreateLeningdeelRequest(
        DateOnly StartDatum,
        int Looptijd,
        RenteVastePeriode RenteVastePeriode,
        Amount Hoofdsom);

    public record CreateAflossingsvrijLeningdeelRequest(
        DateOnly StartDatum, 
        int Looptijd, 
        RenteVastePeriode RenteVastePeriode, 
        Amount Hoofdsom,
        Amount ExtraMaandelijkseAflossing) 
        : CreateLeningdeelRequest(StartDatum, Looptijd, RenteVastePeriode, Hoofdsom);
}
