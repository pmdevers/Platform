
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Core.Features.Projects;

public static class Delete
{
    public static void MapDeleteProject(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/{id}", HandleAsync);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsync(
        [AsParameters] ProjectsServices services,
        [FromHeader(Name = SubscriptionId.HeaderName)] SubscriptionId subscriptionId,
        [FromRoute] ProjectId id
        )
    {
        if(subscriptionId == SubscriptionId.Empty || subscriptionId == SubscriptionId.Unknown)
        {
            return TypedResults.BadRequest();
        }

        var project = await services.Manager.LoadAsync(id);

        if(project is null)
        {
            return TypedResults.BadRequest();
        }

        project.Delete();

        await services.Manager.SaveAsync(project);

        return TypedResults.Ok();
    }
}
