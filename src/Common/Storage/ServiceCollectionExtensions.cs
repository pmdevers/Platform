using FinSecure.Platform.Common.Storage;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IAggregateBuilder<TState> AddAggregate<TState>(this IServiceCollection services, TState initialState)
    {
        var builder = AggregateBuilder.Create(initialState);
        services.AddSingleton(builder);
        services.AddScoped(x => x.GetRequiredService<AggregateBuilder<TState>>().Build(x));
        return builder;
    }
}
