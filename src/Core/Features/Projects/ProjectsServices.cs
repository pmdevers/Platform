using Confluent.Kafka;
using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Core.Domain.Projects;

namespace FinSecure.Platform.Core.Features.Projects;

public class ProjectsServices(IAggregateManager<ProjectAggregate, ProjectId> manager)
{
    public IAggregateManager<ProjectAggregate, ProjectId> Manager { get; } = manager;
}
