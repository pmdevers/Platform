using Featurize.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace FinSecure.Platform.Common.Telemetry;
public class OpenTelemetryFeature : IHostFeature
{
    public void Configure(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging(logging =>
        {
            logging.AddOpenTelemetry(options =>
            {
                options.IncludeScopes = true;
                options.IncludeFormattedMessage = true;
            });
        });

        hostBuilder.ConfigureServices((h, services) =>
        {
            services.AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics.AddRuntimeInstrumentation();
                    metrics.AddMeter(
                        "Microsoft.AspNetCore.Hosting",
                        "Microsoft.AspNetCore.Server.Kestrel",
                        "System.Net.Http");
                })
            .WithTracing(tracing =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (environment == "Development")
                {
                    // We want to view all traces in development
                    tracing.SetSampler(new AlwaysOnSampler());
                }

                tracing.AddAspNetCoreInstrumentation()
                       .AddGrpcClientInstrumentation()
                       .AddHttpClientInstrumentation();
            });

            var useOtlpExporter = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT");

            if (!string.IsNullOrEmpty(useOtlpExporter))
            {
                services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
                services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
                services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
            }
        });
    }
}
