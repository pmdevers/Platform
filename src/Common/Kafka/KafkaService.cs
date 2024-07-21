using Microsoft.Extensions.Hosting;

namespace FinSecure.Platform.Common.Kafka;
internal class KafkaService(IEnumerable<IKafkaProcess> processes) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var process in processes)
        {
            process.Start();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var process in processes)
        {
            process.Stop();
        }

        return Task.CompletedTask;
    }
}
