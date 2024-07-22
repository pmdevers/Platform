using FinSecure.Platform.Common.Kafka.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Kafka;

public static class KafkaExtensions 
{
    public static IServiceCollection AddMinimalKafka(this IServiceCollection services, Action<IKafkaConventionBuilder> config)
    {
        var conventions = new List<Action<IKafkaBuilder>>();
        var configBuilder = new KafkaConventionBuilder(conventions, []);

        config(configBuilder);

        services.AddSingleton<IKafkaBuilder>(s => {
            var b = new KafkaBuilder(s);
            conventions.ForEach(x => x(b));
            return b;
        });
        services.AddHostedService<KafkaService>();

        return services;
    }

    public static IKafkaConventionBuilder MapTopic(this IApplicationBuilder builder, string topic, Delegate handler)
    {
        var tb = builder.ApplicationServices.GetRequiredService<IKafkaBuilder>();
        return tb.MapTopic(topic, handler);
    }
    public static IKafkaConventionBuilder MapTopic(this IKafkaBuilder builder, string topic, Delegate handler)
    {
        return builder
            .GetOrAddTopicDataSource()
            .AddTopicDelegate(topic, handler)
            .WithMetaData([..builder.MetaData]);
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
