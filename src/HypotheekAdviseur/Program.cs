using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Kafka.Extension;

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
}).WithGroupId("Topic 1");

app.MapTopic("test1", (
    KafkaContext context,
    ILogger<KafkaContext> logger,
    string key,
    string value) =>
{
    logger.LogInformation("{Key} - {Value}", key, value);
    return Task.CompletedTask;
}).WithGroupId("Topic 2");

app.MapGet("/", () => "").WithName("test");

await app.RunAsync();
