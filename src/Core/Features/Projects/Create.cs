
using Confluent.Kafka;
using Featurize.ValueObjects;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Logging;
using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Core.Domain.Projects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalKafka;
using MinimalKafka.Serializers;
using System.Text.Json;

namespace FinSecure.Platform.Core.Features.Projects;

public static class Create
{
    public static void MapCreateProject(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", HandleAsync)
            .WithDescription("Queues a project to be created. Use the GetOperation to periodically check for create project status.");

        builder.MapTopic("projects", HandleTopicAsync);
    }

    private static async Task<Results<Ok, BadRequest>> HandleAsync(
        [FromHeader(Name = Headers.SubscriptionHeader)] SubscriptionId subscriptionId,
        [FromBody] CreateProjectRequest request
        )
    {
        if (subscriptionId.IsEmptyOrUnknown())
        {
            return TypedResults.BadRequest();
        }

        var config = new ProducerConfig()
        {
            BootstrapServers = "nas.home.lab:9092",
        };
        var producer = new ProducerBuilder<ProjectId, ValidatedProjectRequest>(config)
            .SetKeySerializer(new JsonTextSerializer<ProjectId>(JsonSerializerOptions.Default))
            .SetValueSerializer(new JsonTextSerializer<ValidatedProjectRequest>(JsonSerializerOptions.Default))
            .Build();

        await producer.ProduceAsync("projects", new Message<ProjectId, ValidatedProjectRequest>
        {
            Key = request.ProjectId,
            Value = new(subscriptionId, request.ProjectId, request.Name),
        });
              

        return TypedResults.Ok();
    }

    public record CreateProjectRequest(ProjectId ProjectId, string Name);
    public record ValidatedProjectRequest(SubscriptionId SubscriptionId, ProjectId ProjectId, string Name);

    public static async Task HandleTopicAsync(
        AggregateManager<Project, ProjectId> manager, 
        ProjectId key,
        ValidatedProjectRequest value,
        ILogger<Project> logger) 
    {
        logger.LogInformation("Project Recieved: {Key} - {Name}", key, value);
        var project = Project.Create(key, value.SubscriptionId, value.Name);
        await manager.SaveAsync(project);
    }
}
