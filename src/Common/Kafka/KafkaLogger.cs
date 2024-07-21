using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Kafka;
public class KafkaLogger
{
    public KafkaLogger(ILogger<KafkaLogger> logger)
    {
        Logger = logger;
        Instance = this;
    }

    public static KafkaLogger? Instance { get; private set; }

    public ILogger<KafkaLogger> Logger { get; }

}
