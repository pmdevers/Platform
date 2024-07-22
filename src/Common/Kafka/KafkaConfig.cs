using Confluent.Kafka;

namespace FinSecure.Platform.Common.Kafka;

public class KafkaConfig
{
    public ConsumerConfig ConsumerConfig { get; set; } = new();
}
