using Confluent.Kafka;
using FinSecure.Platform.HypotheekAdviseur;
using MinimalKafka;
using MinimalKafka.Extension;
using MinimalKafka.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .DiscoverFeatures();

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
});

await app.RunAsync();
