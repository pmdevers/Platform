using FinSecure.Platform.Common.Helpers;

namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IGroupIdMetadata
{
    string GroupId { get; }
}

public class GroupIdMetadata(string name) : IGroupIdMetadata
{
    public string GroupId { get; } = name;
    
    /// <inheritdoc/>
    public override string ToString()
    {
        return DebuggerHelpers.GetDebugText(nameof(GroupId), GroupId);
    }
}