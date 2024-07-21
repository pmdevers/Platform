﻿using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Kafka.Builders;

public interface IKafkaBuilder
{
    IServiceProvider ServiceProvider { get; }
    ILogger Logger { get; }
    KafkaDataSource? DataSource { get; set; }
    List<object> MetaData { get; }
}
