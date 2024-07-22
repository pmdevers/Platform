using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Kafka.Extension;
using FinSecure.Platform.Common.Kafka.Metadata;
using FinSecure.Platform.Common.Kafka.Serializers;
using FinSecure.Platform.HypotheekAdviseur;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .DiscoverFeatures();

builder.Services.AddMinimalKafka(config => 
{ 
    config.WithBootstrapServers("nas.home.lab:9092")
          .WithGroupId(Guid.NewGuid().ToString())
          .WithOffsetReset(AutoOffsetReset.Earliest); 
});

var app = builder.BuildWithFeatures();

app.MapTopic("test", (
    KafkaContext context, 
    ILogger<KafkaContext> logger,
    string key,
    string value) =>
{
    logger.LogInformation("{Key} - {Value}", key, value);
    return Task.CompletedTask;
})
.WithGroupId("Topic 1")
.WithKeySerializer(Deserializers.Utf8)
.WithValueSerializer(Deserializers.Utf8)
.WithReportInterval(1);

app.MapTopic("test1", (
    KafkaContext context,
    ILogger<KafkaContext> logger,
    [FromKey] Test result,
    Test value) =>
{
    logger.LogInformation("{Key} - {Value}", result, value);
    return Task.CompletedTask;
}).WithGroupId("Topic 2");

await app.RunAsync();
