using Confluent.Kafka;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MinimalKafka;
using MinimalKafka.Builders;
using MinimalKafka.Extension;
using System.Diagnostics.CodeAnalysis;

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

public static class MinimalKafkaExtensions
{
    public static IKafkaConventionBuilder MapTopic(this IEndpointRouteBuilder builder, 
        [StringSyntax("route")] string topic, KafkaDelegate handler)
    {
        var kafkaBuilder = builder.ServiceProvider.GetRequiredService<IKafkaBuilder>();
        return kafkaBuilder.MapTopic(topic, handler);
    }

    public static IKafkaConventionBuilder MapTopic(this IEndpointRouteBuilder builder,
        [StringSyntax("route")] string topic, Delegate handler)
    {
        var kafkaBuilder = builder.ServiceProvider.GetRequiredService<IKafkaBuilder>();
        return kafkaBuilder.MapTopic(topic, handler);
    }
}
