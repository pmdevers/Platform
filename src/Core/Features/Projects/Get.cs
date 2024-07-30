using Featurize.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Core.Features.Projects;

public static class Get
{
    public static void MapGetProject(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{projectId}", HandleAsync);
    }

    private static async Task<Results<Ok<GetProjectResponse>, BadRequest, NotFound>> HandleAsync(
        [AsParameters] ProjectsServices services,
        [FromHeader(Name = Headers.SubscriptionHeader)] SubscriptionId subscriptionId,
        [FromRoute] ProjectId projectId
        )
    {
        if (subscriptionId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var project = await services.Manager.LoadAsync(projectId);

        if(project is null) 
        {
            return TypedResults.NotFound();
        }

        if(project.State.SubscriptionId != subscriptionId)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GetProjectResponse(project.Id, project.State.Name, project.LastModifiedOn));
    }

    public record GetProjectResponse(ProjectId Id, string Name, DateTimeOffset? LastModifiedOn);
}
