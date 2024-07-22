using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Kafka.Builders;
using FinSecure.Platform.Common.Kafka.Extension;
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

        services.AddMinimalKafka(c =>
            c.WithBootstrapServers("nas.home.lab:9092")
             .WithGroupId("sdsd")
             .WithOffsetReset(AutoOffsetReset.Earliest)
            );

        var provider = services.BuildServiceProvider();
        
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
            Metadata = result.Metadata,
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
