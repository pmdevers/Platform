using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Kafka.Builders;

public class KafkaBuilder(IServiceProvider serviceProvider, ILogger<KafkaBuilder> logger) : IKafkaBuilder
{
    public IServiceProvider ServiceProvider { get; } = serviceProvider;
    public ILogger Logger { get; } = logger;

    public KafkaDataSource? DataSource { get; set; }

    public List<object> MetaData { get; } = [];
    
}

