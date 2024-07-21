﻿using Confluent.Kafka;

namespace FinSecure.Platform.Common.Kafka;
public abstract class KafkaContext
{
    public abstract object? Key { get; }
    public abstract object? Value { get; }
    public abstract Headers Headers { get; }

    public abstract IServiceProvider RequestServices { get; }

    public static KafkaContext Empty { get; } = new EmptyKafkaContext();

    public static KafkaContext Create(object result, IServiceProvider serviceProvider)
    {
        if (result.GetType().GetGenericTypeDefinition() != (typeof(ConsumeResult<,>).GetGenericTypeDefinition()))
        {
            return Empty;
        }

        var keyType = result.GetType().GenericTypeArguments[0];
        var valueType = result.GetType().GenericTypeArguments[1];
        

        var creator = typeof(KafkaContext<,>).MakeGenericType(keyType, valueType)
            .GetConstructor([ result.GetType(), typeof(IServiceProvider) ]);

        return (KafkaContext)(
            creator?.Invoke([result, serviceProvider]) ??
            Empty
        );
    }
}

internal class EmptyKafkaContext : KafkaContext
{
    public override object? Key => null;

    public override object? Value => null;

    public override Headers Headers => [];

    public override IServiceProvider RequestServices => EmptyServiceProvider.Instance;
}

internal class KafkaContext<TKey, TValue>(ConsumeResult<TKey, TValue> result, IServiceProvider serviceProvider) : KafkaContext
{
    public override object? Key => result.Message.Key;

    public override object? Value => result.Message.Value;

    public override Headers Headers => result.Message.Headers;

    public override IServiceProvider RequestServices => serviceProvider;
}