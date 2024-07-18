
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

    private static async Task<Ok, BadRequest> HandleAsync(
        [AsParameters] LeningenServices services,
        [FromBody] CreateLeningRequest request
        )
    {

        var lening = Lening.Create();



    }

    public record CreateLeningRequest(LeningId LeningId);
}
