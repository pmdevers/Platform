using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Kafka.Builders;

public sealed class KafkaDataSource(IServiceProvider serviceProvider)
{
    private readonly List<KafkaProcessEntry> _entries = [];

    public IServiceProvider ServiceProvider => serviceProvider;

    public KafkaConventionBuilder AddTopicDelegate(string topicName, Delegate handler)
    {
        var conventions = new AddAfterProcessBuildConventionCollection();
        var finallyConventions = new AddAfterProcessBuildConventionCollection();

        _entries.Add(new()
        {
            TopicName = topicName,
            Delegate = handler,
            Conventions = conventions,
            FinallyConventions = finallyConventions,
        });

        return new KafkaConventionBuilder(conventions, finallyConventions);
    }

    public IEnumerable<KafkaProcess> GetProceses()
    {
        foreach (var process in _entries)
        {
            var result = KafkaDelegateFactory.Create(process.Delegate, new()
            {
                ServiceProvider = serviceProvider,
            });

            var config = new ConsumerConfig
            {
                BootstrapServers = "nas.home.lab:9092",
                GroupId = Guid.NewGuid().ToString(), // Use type name for unique group ID
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            var consumer = KafkaConsumer.Create(new()
            {
                KeyType = result.KeyType,
                ValueType = result.ValueType,
                ServiceProvider = serviceProvider,
                TopicName = process.TopicName,
                Config = config,
            });

            yield return KafkaProcess.Create(new()
            {
                Consumer = consumer,
                Delegate = result.Delegate,
            });
        }
    }

    private struct KafkaProcessEntry()
    {
        public string TopicName { get; set; } = string.Empty;
        public Delegate Delegate { get; set; } = () => Task.CompletedTask;
        public ConsumerConfig ConsumerConfig { get; set; } = [];

        public AddAfterProcessBuildConventionCollection Conventions { get; init; }
        public AddAfterProcessBuildConventionCollection FinallyConventions { get; init; }
    }
    private sealed class AddAfterProcessBuildConventionCollection :
        List<Action<IKafkaBuilder>>,
        ICollection<Action<IKafkaBuilder>>
    {
        public bool IsReadOnly { get; set; }

        void ICollection<Action<IKafkaBuilder>>.Add(Action<IKafkaBuilder> convention)
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException($"{nameof(KafkaDataSource)} can not be modified after build.");
            }

            Add(convention);
        }
    }
}
