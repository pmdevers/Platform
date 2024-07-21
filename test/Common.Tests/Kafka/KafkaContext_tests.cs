using Confluent.Kafka;
using FinSecure.Platform.Common.Kafka;

namespace Common.Tests.Kafka;
public class KafkaContext_tests
{
    [Fact]
    public void Return_EmptyContext_if_not_ConsumeResult()
    {
        var test = "";
        var context = KafkaContext.Create(test);

        context.Should().Be(KafkaContext.Empty);
    }

    [Fact]
    public void Return_KafkaContext_if_consumeResult()
    {
        var result = new ConsumeResult<string, string>() {

            Message = new Message<string, string>()
            {
                Value = "Test",
                Key = "Test",
                Headers = []
            }
        };

        var context = KafkaContext.Create(result);

        context.Key.Should().Be("Test");
        context.Value.Should().Be("Test");
        context.Headers.Should().BeEmpty();
        
    }

}
