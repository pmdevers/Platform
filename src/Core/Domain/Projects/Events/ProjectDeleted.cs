using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Core.Domain.Projects.Events;

public record ProjectDeleted(ProjectId ProjectId, bool Deleted) : Event;
