namespace FinSecure.Platform.Core.Domain.Projects.Events;

public record ProjectDeleted(ProjectId ProjectId) : EventRecord;
