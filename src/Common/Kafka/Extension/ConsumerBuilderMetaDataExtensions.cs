﻿using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka.Builders;
using FinSecure.Platform.Common.Kafka.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Kafka.Extension;
public static class ConsumerBuilderMetaDataExtensions
{
    

    public static TBuilder WithKeySerializer<TBuilder>(this TBuilder builder, Func<IServiceProvider, Type, object> serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithSingle(new KeyDeserializerMetaData(serializer));
        return builder;
    }

    public static TBuilder WithKeySerializer<TBuilder>(this TBuilder builder, Type serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        if (!serializer.IsTheGenericType(typeof(IDeserializer<>)))
        {
            throw new InvalidOperationException($"Type '{serializer}' should of type '{typeof(IDeserializer<>)}'");
        }
        builder.WithKeySerializer((s, t) => s.GetRequiredService(serializer.MakeGenericType(t)));
        return builder;
    }

    public static TBuilder WithKeySerializer<TBuilder, T>(this TBuilder builder, IDeserializer<T> serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithKeySerializer((s, t) => serializer);
        return builder;
    }
    
    public static TBuilder WithValueSerializer<TBuilder>(this TBuilder builder, Func<IServiceProvider, Type, object> serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithSingle(new ValueDeserializerMetaData(serializer));
        return builder;
    }

    public static TBuilder WithValueSerializer<TBuilder>(this TBuilder builder, Type serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        if (!serializer.IsTheGenericType(typeof(IDeserializer<>)))
        {
            throw new InvalidOperationException($"Type '{serializer}' should of type '{typeof(IDeserializer<>)}'");
        }
        builder.WithValueSerializer((s, t) => s.GetRequiredService(serializer.MakeGenericType(t)));
        return builder;
    }

    public static TBuilder WithValueSerializer<TBuilder, T>(this TBuilder builder, IDeserializer<T> serializer)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithValueSerializer((s, t) => serializer);
        return builder;
    }

    public static bool IsTheGenericType(this Type candidateType, Type genericType)
    {
        return
            candidateType != null && genericType != null &&
            (candidateType.IsGenericType && candidateType.GetGenericTypeDefinition() == genericType ||
             candidateType.GetInterfaces().ToList().Exists(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType) ||
             candidateType.BaseType != null && candidateType.BaseType.IsTheGenericType(genericType));
    }
}
