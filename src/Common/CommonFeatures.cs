using FinSecure.Platform.Common.HealthChecks;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.OpenApi;
using FinSecure.Platform.Common.Serialization;
using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Common.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace FinSecure.Platform.Common;

public static class CommonFeatures
{
    public static IFeatureCollection AddCommonFeatures(this WebApplicationBuilder builder)
    {

        return builder.Features()
            .AddCommonFeatures(builder.Configuration);
    }

    public static IFeatureCollection AddCommonFeatures(this IFeatureCollection features, IConfiguration configuration)
    {
        features
            .AddHealthChecks()
            .AddOpenTelemetry()
            .AddSerializaion()
            .AddOpenApi()
            .AddKafka(configuration);
        return features;
    }

    public static IFeatureCollection AddOpenApi(this IFeatureCollection features)
    {
        features.Add<OpenApiFeature>();
        return features;
    }

    public static IFeatureCollection AddSerializaion(this IFeatureCollection features)
    {
        features.Add<SerializationFeature>();
        return features;
    }

    public static IFeatureCollection AddHealthChecks(this IFeatureCollection features)
    {
        features.Add<HealthChecksFeature>();
        return features;
    }

    public static IFeatureCollection AddOpenTelemetry(this IFeatureCollection features)
    {
        features.Add<OpenTelemetryFeature>();
        return features;
    }

    public static IFeatureCollection AddKafka(this IFeatureCollection features, IConfiguration config)
    {
        features.AddWithOptions<KafkaFeature, KafkaFeatureOptions>(x => config.Bind("kafka", x));
        return features;
    }
}
