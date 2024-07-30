using Featurize.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StreamWave;

namespace FinSecure.Platform.Common.Storage;
internal class StorageFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddAggregate<TestAggregate, Guid>(() => new());
    }

    public void Use(WebApplication app)
    {
    }
}

public class TestAggregate : IAggregateState<Guid>
{
    public Guid Id { get; set; } = Guid.Empty;
}

