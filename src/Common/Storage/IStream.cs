namespace FinSecure.Platform.Common.Storage;

public interface IEventStream : IReadOnlyCollection<Event>
{
    Guid Id { get; }
    int Version { get; }
    int ExpectedVersion { get; }
    DateTimeOffset? CreatedOn { get; }
    DateTimeOffset? LastModifiedOn { get; }
    void Append(Event e);
    Event[] GetUncommittedEvents();
    IEventStream Commit();
}

public abstract record Event
{
    public DateTimeOffset OccouredOn { get; set; } = DateTimeOffset.UtcNow;
}
