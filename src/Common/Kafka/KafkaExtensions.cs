﻿using FinSecure.Platform.Common.Kafka.Builders;
using FinSecure.Platform.Common.Kafka.Extension;
using FinSecure.Platform.Common.Kafka.Serializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Kafka;

public static class KafkaExtensions 
{
    public static IServiceCollection AddMinimalKafka(this IServiceCollection services, Action<IKafkaConventionBuilder> config)
    {
        var conventions = new List<Action<IKafkaBuilder>>();
        var configBuilder = new KafkaConventionBuilder(conventions, []);

        configBuilder.WithKeySerializer(typeof(JsonTextSerializer<>));
        configBuilder.WithValueSerializer(typeof(JsonTextSerializer<>));

        config(configBuilder);

        services.AddTransient(typeof(JsonTextSerializer<>));

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

    public static TBuilder WithSingle<TBuilder>(this TBuilder builder, object metadata)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.RemoveMetaData(metadata);
        builder.WithMetaData(metadata);
        return builder;
    }


    public static TBuilder RemoveMetaData<TBuilder>(this TBuilder builder, object item)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.Add(b =>
        {
            b.MetaData.RemoveAll(x => x.GetType() == item.GetType());
        });

        return builder;
    }

    private static KafkaDataSource GetOrAddTopicDataSource(this IKafkaBuilder builder)
    {
        builder.DataSource ??= new KafkaDataSource(builder.ServiceProvider);
        return builder.DataSource;
    }
}
