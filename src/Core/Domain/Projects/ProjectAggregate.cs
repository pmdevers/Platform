using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Core.Domain.Projects.Events;

namespace FinSecure.Platform.Core.Domain.Projects;

public class ProjectAggregate : OutboxAggregateRoot<ProjectId>
{
    public Project State { get; } = new();

    private ProjectAggregate(ProjectId id) : base(id)
    {
    }

    public static ProjectAggregate Create(ProjectId projectId, SubscriptionId subscriptionId, string name)
    {
        var project = new ProjectAggregate(projectId);

        project.RecordEvent(new ProjectCreated(project.Id, subscriptionId, name));

        project.Outbox.Add(new OutboxMessage("project", project.State));

        return project;
    }

    public void Delete()
    {
        RecordEvent(new ProjectDeleted(Id, true));
    }

    internal void Apply(ProjectCreated e) 
    {
        State.Id = e.ProjectId;
        State.Name = e.Name;
        State.SubscriptionId = e.SubscriptionId;
    }

    internal void Apply(ProjectDeleted e)
    {
        State.IsDeleted = e.Deleted;
    }
}
