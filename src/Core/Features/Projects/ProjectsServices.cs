using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Core.Domain.Projects;

namespace FinSecure.Platform.Core.Features.Projects;

public class ProjectsServices(AggregateManager<Project, ProjectId> manager)
{
    public AggregateManager<Project, ProjectId> Manager { get; } = manager;
}
