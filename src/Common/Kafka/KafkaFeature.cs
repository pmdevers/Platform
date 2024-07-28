using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MinimalKafka;
using MinimalKafka.Extension;

namespace FinSecure.Platform.Common.Kafka;

public class KafkaFeatureOptions
{
    public string BootstrapServers { get; set; } = string.Empty;
}

public class KafkaFeature(KafkaFeatureOptions options)
    : IWebApplicationFeature, 
      IFeatureWithOptions<KafkaFeature, KafkaFeatureOptions>
{
        public KafkaFeatureOptions Options { get; } = options;

        public static KafkaFeature Create(KafkaFeatureOptions options)
            => new(options);

        public void Configure(IServiceCollection services)
    {
        services.AddMinimalKafka(x => {
            x.WithBootstrapServers(Options.BootstrapServers);
        });
    }

    public void Use(WebApplication app)
    {
    }
}
