using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Kafka;
public sealed class KafkaLogger(ILogger<KafkaLogger> logger)
{
    public ILogger<KafkaLogger> Logger { get; } = logger;


}
