using FinSecure.Platform.Common.HealthChecks;
using FinSecure.Platform.Common.Kafka;
using FinSecure.Platform.Common.OpenApi;
using FinSecure.Platform.Common.Serialization;
using FinSecure.Platform.Common.Storage;
using FinSecure.Platform.Common.Telemetry;

namespace FinSecure.Platform.Common;

public static class CommonFeatures
{
    public static IFeatureCollection AddCommonFeatures(this IFeatureCollection features)
    {
        features
            .AddHealthChecks()
            .AddOpenTelemetry()
            .AddSerializaion()
            .AddOpenApi()
            .AddStorage()
            .AddKafka();
        return features;
    }

    public static IFeatureCollection AddOpenApi(this IFeatureCollection features)
    {
        features.Add<OpenApiFeature>();
        return features;
    }

    public static IFeatureCollection AddStorage(this IFeatureCollection features)
    {
        features.Add<StorageFeature>();
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

    public static IFeatureCollection AddKafka(this IFeatureCollection features) 
    {
        features.Add<KafkaFeature>();
        return features;
    }
}
