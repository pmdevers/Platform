using Confluent.Kafka;

namespace FinSecure.Platform.Common.Kafka;

public abstract class Consumer
{
    public abstract void Subscribe(string topicName);
    public abstract KafkaContext Consume(CancellationToken cancellationToken);

    public abstract void Close();

    public static Consumer Create(Type keyType, Type valueType, ConsumerConfig config)
    {
        var creator = typeof(Consumer<,>)
            .MakeGenericType(keyType, valueType)
            .GetConstructor([typeof(ConsumerConfig)]);

        return (Consumer)(creator?.Invoke([config]) ?? new NoConsumer());

    }
}

public class NoConsumer : Consumer
{
    public override void Close()
    {
    }

    public override KafkaContext Consume(CancellationToken cancellationToken)
    {
        return KafkaContext.Empty;
    }
    
    public override void Subscribe(string topicName)
    {
    }
}

public class Consumer<TKey, TValue>(ConsumerConfig config) : Consumer
{
    private readonly IConsumer<TKey, TValue> _consumer = new ConsumerBuilder<TKey, TValue>(config)
            .Build();

    public override KafkaContext Consume(CancellationToken cancellationToken)
    {
        var result = _consumer.Consume(cancellationToken);
        return KafkaContext.Create(result);
    }

    public override void Close()
    {
        _consumer.Close();
        _consumer.Dispose();
    }

    public override void Subscribe(string topicName)
    {
        _consumer.Subscribe(topicName);
    }
}