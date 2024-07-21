using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .DiscoverFeatures();

builder.Services.AddSingleton<Log>();
builder.Services.ActivateSingleton<Log>();

var app = builder.BuildWithFeatures();

app.MapTopic("test", (
    KafkaContext context, 
    ILogger<KafkaContext> logger,
    string key,
    string value) =>
{
    logger.LogInformation("{Key} - {Value}", key, value);
    return Task.CompletedTask;
});

await app.RunAsync();
