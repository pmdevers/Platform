using Confluent.Kafka;
using FinSecure.Platform.Common.Helpers;

namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IGroupIdMetadata
{
    string GroupId { get; }
}

public class GroupIdMetadata(string name) : IGroupIdMetadata, IConsumerConfigMetadata
{
    public string GroupId { get; } = name;

    public void Set(ConsumerConfig config)
    {
        config.GroupId = GroupId;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return DebuggerHelpers.GetDebugText(nameof(GroupId), GroupId);
    }
}