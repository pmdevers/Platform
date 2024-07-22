using Confluent.Kafka;
using FinSecure.Platform.Common.Helpers;

namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IAutoOffsetResetMetadata
{
    public AutoOffsetReset AutoOffsetReset { get; }
}

public class AutoOffsetResetMetadata(AutoOffsetReset autoOffsetReset) : IAutoOffsetResetMetadata
{
    public AutoOffsetReset AutoOffsetReset => autoOffsetReset;

    public override string ToString()
        => DebuggerHelpers.GetDebugText(nameof(AutoOffsetReset), AutoOffsetReset.ToString());
}
