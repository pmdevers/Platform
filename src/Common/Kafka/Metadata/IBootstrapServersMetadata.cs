using Confluent.Kafka;
using FinSecure.Platform.Common.Helpers;

namespace FinSecure.Platform.Common.Kafka.Metadata;
public interface IBootstrapServersMetadata
{
    public string BootstrapServers { get; }
}

public class BootstrapServersMetadata(string bootstrapServers) : IBootstrapServersMetadata, IConsumerConfigMetadata
{
    public string BootstrapServers => bootstrapServers;

    public void Set(ConsumerConfig config)
    {
        config.BootstrapServers = BootstrapServers;
    }

    public override string ToString()
        => DebuggerHelpers.GetDebugText(nameof(BootstrapServers), BootstrapServers);
}
