﻿
using Featurize.ValueObjects;
using FinSecure.Platform.Core.Domain.Projects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinSecure.Platform.Core.Features.Projects;

public static class Create
{
    public static void MapCreateProject(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", HandleAsync)
            .WithDescription("Queues a project to be created. Use the GetOperation to periodically check for create project status.");
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsync(
        [AsParameters] ProjectsServices services,
        [FromHeader(Name = Headers.SubscriptionHeader)] SubscriptionId subscriptionId,
        [FromBody] CreateProjectRequest request
        )
    {
        if (subscriptionId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var project = Project.Create(request.ProjectId, subscriptionId, request.Name);
        await services.Manager.SaveAsync(project);

        await services.Producer.ProduceAsync("project", new()
        {
            Key = request.ProjectId,
            Value = project,
        });

        return TypedResults.Ok();
    }

    public record CreateProjectRequest(ProjectId ProjectId, string Name);
}
