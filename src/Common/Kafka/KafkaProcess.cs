namespace FinSecure.Platform.Common.Kafka;
public interface IKafkaProcess
{
    void Start();
    void Stop();
}

public class KafkaProcess(
    Consumer consumer,
    string topic,
    KafkaDelegate handler
    ) : IKafkaProcess
{
    private readonly CancellationTokenSource _cancellationToken = new();
    public void Start()
    {
        Task.Factory.StartNew(() =>
        {
            consumer.Subscribe(topic);

            while (!_cancellationToken.IsCancellationRequested)
            {
                var context = consumer.Consume(_cancellationToken.Token);

                if (context is EmptyKafkaContext)
                {
                    continue;
                }

                handler.Invoke(context);

            }
        });
    }

    public void Stop()
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();

    }
}