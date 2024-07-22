using FinSecure.Platform.Common.Helpers;

namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IBootstrapServersMetadata
{
    public string BootstrapServers { get; }
}

public class BootstrapServersMetadata(string bootstrapServers) : IBootstrapServersMetadata
{
    public string BootstrapServers => bootstrapServers;

    public override string ToString()
        => DebuggerHelpers.GetDebugText(nameof(BootstrapServers), BootstrapServers);
}
