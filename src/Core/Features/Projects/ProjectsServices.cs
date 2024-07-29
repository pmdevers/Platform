using Confluent.Kafka;

using FinSecure.Platform.Core.Domain.Projects;
using StreamWave;

namespace FinSecure.Platform.Core.Features.Projects;

public class ProjectsServices(IAggregate<ProjectAggregate, ProjectId> manager)
{
    public IAggregate<ProjectAggregate, ProjectId> Manager { get; } = manager;
}
