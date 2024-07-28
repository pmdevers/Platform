namespace FinSecure.Platform.Common.Storage;

public interface IStreamManger
{
    Task<IEventStream> LoadAsync(Guid streamId);
}

public class StreamManger : IStreamManger
{
    private readonly Dictionary<Guid, Event[]> _streams = [];

    public Task<IEventStream> LoadAsync(Guid streamId)
    {
        
    }
}
