using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka.Builders;
using FinSecure.Platform.Common.Kafka.Metadata;

namespace FinSecure.Platform.Common.Kafka.Extension;
public static class MapTopicExtensions
{
    public static TBuilder WithGroupId<TBuilder>(this TBuilder builder, string groupId)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithMetaData(new GroupIdMetadata(groupId));
        return builder;
    }

    public static TBuilder WithBootstrapServers<TBuilder>(this TBuilder builder, string bootstrapServers)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithMetaData(new BootstrapServersMetadata(bootstrapServers));
        return builder;
    }

    public static TBuilder WithOffsetReset<TBuilder>(this TBuilder builder, AutoOffsetReset autoOffsetReset)
        where TBuilder : IKafkaConventionBuilder
    {
        builder.WithMetaData(new AutoOffsetResetMetadata(autoOffsetReset));
        return builder;
    }

    
}
