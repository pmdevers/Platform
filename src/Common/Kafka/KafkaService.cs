using FinSecure.Platform.Common.Kafka.Builders;
using Microsoft.Extensions.Hosting;

namespace FinSecure.Platform.Common.Kafka;
internal class KafkaService(IKafkaBuilder builder) : IHostedService
{
    public IEnumerable<KafkaProcess> Processes 
        => builder.DataSource?.GetProceses() ?? [];

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var process in Processes)
        {
            process.Start();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var process in Processes)
        {
            process.Stop();
        }

        return Task.CompletedTask;
    }
}
