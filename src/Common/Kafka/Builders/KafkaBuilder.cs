﻿namespace FinSecure.Platform.Common.Kafka.Builders;

public class KafkaBuilder(IServiceProvider serviceProvider) : IKafkaBuilder
{
    public IServiceProvider ServiceProvider { get; } = serviceProvider;
    public KafkaDataSource? DataSource { get; set; }   
    public List<object> MetaData { get; } = [];
}
