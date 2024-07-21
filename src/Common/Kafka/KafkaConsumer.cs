using Confluent.Kafka;
using FinSecure.Platform.Common.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace FinSecure.Platform.Common.Kafka;

public abstract class KafkaConsumer
{
    public abstract void Subscribe();
    public abstract KafkaContext Consume(CancellationToken cancellationToken);

    public abstract void Close();

    public static KafkaConsumer Create(KafkaConsumerOptions options)
    {
        var creator = typeof(KafkaConsumer<,>)
            .MakeGenericType(options.KeyType, options.ValueType)
            .GetConstructor([typeof(KafkaConsumerOptions)]);

        return (KafkaConsumer)(creator?.Invoke([options]) ?? new NoConsumer());
    }
}

public class KafkaConsumerOptions
{
    public required Type KeyType { get; init; } = typeof(Ignore);
    public required Type ValueType { get; init; } = typeof(Ignore);
    public required string TopicName { get; init; } = string.Empty;
    public required ConsumerConfig Config { get; init; } = [];
    public required IServiceProvider ServiceProvider { get; init; } = EmptyServiceProvider.Instance;
}

public class NoConsumer : KafkaConsumer
{
    public override void Close()
    {
    }

    public override KafkaContext Consume(CancellationToken cancellationToken)
    {
        return KafkaContext.Empty;
    }
    
    public override void Subscribe()
    {
    }
}

public class KafkaConsumer<TKey, TValue>(KafkaConsumerOptions options) : KafkaConsumer
{
    private readonly IServiceProvider _serviceProvider = options.ServiceProvider;
    private readonly string _topicName = options.TopicName;
    private readonly IConsumer<TKey, TValue> _consumer = 
        new ConsumerBuilder<TKey, TValue>(options.Config)
            .SetPartitionsRevokedHandler((c, partitions) =>
            {
                KafkaLogger.Instance?.Logger
                .LogInformation("consumer {MemberId} had partitions {Partitions} revoked"
                , c.MemberId, string.Join(",", partitions));
            })
            .SetPartitionsAssignedHandler((c, partitions) =>
            {
                KafkaLogger.Instance?.Logger
                .LogInformation("consumer {MemberId} had partitions {Partitions} assigned"
                , c.MemberId, string.Join(",", partitions));
            })
            .Build();

    public override KafkaContext Consume(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();
        var result = _consumer.Consume(cancellationToken);
        return KafkaContext.Create(result, scope.ServiceProvider);
    }

    public override void Close()
    {
        _consumer.Close();
        _consumer.Dispose();
    }

    public override void Subscribe()
    {
        _consumer.Subscribe(_topicName);
    }
}