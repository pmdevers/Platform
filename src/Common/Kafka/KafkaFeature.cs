using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MinimalKafka;
using MinimalKafka.Extension;

namespace FinSecure.Platform.Common.Kafka;
internal class KafkaFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddMinimalKafka(x => x.WithBootstrapServers("nas.home.lab:9092"));
    }

    public void Use(WebApplication app)
    {
    }
}
