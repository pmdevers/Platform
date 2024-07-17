using Featurize.Repositories;
using Featurize.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace FinSecure.Platform.Common.Storage;

public class StorageFeature : IServiceCollectionFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped(typeof(AggregateManager<,>));
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddInMemory();
    }
}

public static class RepositoryProviderOptionsExtensions
{
    public static RepositoryProviderOptions AddAggregate<TId>(this RepositoryProviderOptions options)
    {
        options.AddRepository<PersistendEvent<TId>, Guid>(o =>
        {
            o.Provider(InMemoryRepositoryProvider.DefaultName);
        });

        return options;
    }

}
