namespace FinSecure.Platform.Common.Kafka;
public interface IKafkaProcess
{
    void Start();
    void Stop();
}

public class KafkaProcessOptions
{
    public KafkaConsumer Consumer { get; set; } = new NoConsumer();
    public KafkaDelegate Delegate { get; set; } = (context) => Task.CompletedTask;
}
public class KafkaProcess : IKafkaProcess
{
    private readonly CancellationTokenSource _cancellationToken = new();
    private readonly KafkaConsumer _consumer;
    private readonly KafkaDelegate _handler;

    private KafkaProcess(
        KafkaConsumer consumer,
        KafkaDelegate handler
    )
    {
        _consumer = consumer;
        _handler = handler;
    }

    public static KafkaProcess Create(KafkaProcessOptions options)
        => new(options.Consumer, options.Delegate);

    public void Start()
    {
        Task.Factory.StartNew(() =>
        {
            _consumer.Subscribe();

            while (!_cancellationToken.IsCancellationRequested)
            {
                var context = _consumer.Consume(_cancellationToken.Token);

                if (context is EmptyKafkaContext)
                {
                    continue;
                }

                _handler.Invoke(context);

            }
        });
    }

    public void Stop()
    {
        _consumer.Close();
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();

    }
}