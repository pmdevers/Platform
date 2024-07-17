using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FinSecure.Platform.Common.Serialization;
internal class SerializationFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => Init(options.SerializerOptions));
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options => Init(options.JsonSerializerOptions));
        services.AddSingleton(Init(new(JsonSerializerDefaults.Web)));
    }

    private static JsonSerializerOptions Init(JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }
}
