using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Kafka.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests.Kafka;
public class KafkaDelegateFactory_Tests
{
    [Fact]
    public void Create()
    {
        var services = new ServiceCollection();
        var provider = services.BuildServiceProvider();
        var config = new ConsumerConfig
        {
            BootstrapServers = "nas.home.lab:9092",
            GroupId = "hhhh1", // Use type name for unique group ID
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        var result = KafkaDelegateFactory.Create(Handle, new()
        {
            ServiceProvider = provider,
            KafkaBuilder = new KafkaBuilder(provider)
        });

        var consumer = KafkaConsumer.Create(new()
        {
            KeyType = result.KeyType,
            ValueType = result.ValueType,
            ServiceProvider = provider,
            TopicName = "test",
            Config = config
        });

        var process = KafkaProcess.Create(new()
        {
            Consumer = consumer,
            Delegate = result.Delegate,
        });

        Assert.NotNull(process);    

    }

    private static Task Handle(KafkaContext context, string value)
    {
        Debug.Write(context.Value);
        Debug.Write(value);

        return Task.CompletedTask;
    }
}
