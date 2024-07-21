using Confluent.Kafka;

namespace FinSecure.Platform.Common.Kafka;
public abstract class KafkaContext
{
    public abstract object? Key { get; }
    public abstract object? Value { get; }
    public abstract Headers Headers { get; }

    public static KafkaContext Empty { get; } = new EmptyKafkaContext();

    public static KafkaContext Create(object result)
    {
        if (result.GetType().GetGenericTypeDefinition() != (typeof(ConsumeResult<,>).GetGenericTypeDefinition()))
        {
            return Empty;
        }

        var keyType = result.GetType().GenericTypeArguments[0];
        var valueType = result.GetType().GenericTypeArguments[1];
        

        var creator = typeof(KafkaContext<,>).MakeGenericType(keyType, valueType)
            .GetConstructor([ result.GetType() ]);

        return (KafkaContext)(
            creator?.Invoke([result]) ??
            Empty
        );
    }
}

internal class EmptyKafkaContext : KafkaContext
{
    public override object? Key => null;

    public override object? Value => null;

    public override Headers Headers => [];
}

internal class KafkaContext<TKey, TValue>(ConsumeResult<TKey, TValue> result) : KafkaContext
{
    public override object? Key => result.Message.Key;

    public override object? Value => result.Message.Value;

    public override Headers Headers => result.Message.Headers;
}