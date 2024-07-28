namespace FinSecure.Platform.Core.Domain.Projects;

public class Project
{
    public ProjectId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubscriptionId SubscriptionId { get; set; } = SubscriptionId.Unknown;
    public bool IsDeleted { get; set; }
}
