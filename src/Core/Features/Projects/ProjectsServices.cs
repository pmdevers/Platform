using Confluent.Kafka;
using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Core.Domain.Projects;

namespace FinSecure.Platform.Core.Features.Projects;

public class ProjectsServices(AggregateManager<Project, ProjectId> manager, IProducer<ProjectId, Project> producer)
{
    public AggregateManager<Project, ProjectId> Manager { get; } = manager;
    public IProducer<ProjectId, Project> Producer { get; } = producer;
}
