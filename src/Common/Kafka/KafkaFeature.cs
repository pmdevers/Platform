using FinSecure.Platform.Common.Kafka.Builders;
using FinSecure.Platform.Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Kafka;
public class KafkaFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddSingleton<KafkaLogger>();
        services.ActivateSingleton<KafkaLogger>();
        services.AddSingleton<IKafkaBuilder, KafkaBuilder>();
        services.AddHostedService<KafkaService>();
    }
}

public static class KafkaExtension 
{
    public static IKafkaConventionBuilder MapTopic(this IApplicationBuilder builder, string topic, Delegate handler)
    {
        var tb = builder.ApplicationServices.GetRequiredService<IKafkaBuilder>();
        return tb.MapTopic(topic, handler);
    }
    public static IKafkaConventionBuilder MapTopic(this IKafkaBuilder builder, string topic, Delegate handler)
    {
        return builder.GetOrAddTopicDataSource().AddTopicDelegate(topic, handler);
    }

    public static TBuilder WithMetaData<TBuilder>(this TBuilder builder, params object[] items) 
        where TBuilder : IKafkaConventionBuilder
    {
        builder.Add(b =>
        {
            foreach (var item in items)
            {
                b.MetaData.Add(item);
            }
        });

        return builder;
    }

    private static KafkaDataSource GetOrAddTopicDataSource(this IKafkaBuilder builder)
    {
        builder.DataSource ??= new KafkaDataSource(builder.ServiceProvider);
        return builder.DataSource;
    }
}
