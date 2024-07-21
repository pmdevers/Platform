using Featurize.AspNetCore;
using FinSecure.Platform.Common.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinSecure.Platform.Common.Logging;

public class Log
{
    private static ILogger? Logger { get; set; }
#pragma warning disable S1118 // Utility classes should not have public constructors
    public Log(ILogger<Log> logger)
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        Logger = logger;
    }

    public static void Error(string message)
    {
        Logger?.LogError(message);
    }
}

internal class LoggingFeature : Featurize.AspNetCore.IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Use(WebApplication app)
    {
        
    }
}
