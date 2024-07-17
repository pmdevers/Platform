using Featurize.DomainModel;
using FinSecure.Platform.Core.Domain.Projects.Events;

namespace FinSecure.Platform.Core.Domain.Projects;

public class Project : AggregateRoot<ProjectId>
{
    public string Name { get; private set; } = string.Empty;
    public SubscriptionId SubscriptionId { get; private set; } = SubscriptionId.Unknown;
    public bool IsDeleted { get; private set; }

    private Project(ProjectId id) : base(id)
    {
    }

    public static Project Create(ProjectId projectId, SubscriptionId subscriptionId, string name)
    {
        var project = new Project(projectId);

        project.RecordEvent(new ProjectCreated(project.Id, subscriptionId, name));

        return project;
    }

    public void Delete()
    {
        RecordEvent(new ProjectDeleted(Id));
    }

    internal void Apply(ProjectCreated e) 
    {
        Name = e.Name;
        SubscriptionId = e.SubscriptionId;
    }

    internal void Apply(ProjectDeleted e)
    {
        IsDeleted = true;
    }
}
