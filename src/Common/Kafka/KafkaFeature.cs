using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka.Extension;
using FinSecure.Platform.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Kafka;
public class KafkaFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddMinimalKafka(config =>
        {
            config.WithBootstrapServers("nas.home.lab:9092");
            config.WithGroupId(Guid.NewGuid().ToString());
            config.WithOffsetReset(AutoOffsetReset.Earliest);
        });
    }
}
