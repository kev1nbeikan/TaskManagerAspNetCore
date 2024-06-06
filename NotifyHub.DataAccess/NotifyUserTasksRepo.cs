using Confluent.Kafka;
using NotifyHub.Core;
using NotifyHub.Core.Abstractions;

namespace NotifyHub.DataAccess;

public class NotifyUserTasksRepo : INotifyUserTasksRepo
{
    private readonly ConsumerConfig _config = new()
    {
        BootstrapServers = "localhost:29092",
        GroupId = "UserTasks",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    private readonly IConsumer<Ignore, string> _consumer;

    public NotifyUserTasksRepo()
    {
        _consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        _consumer.Subscribe("UserTasks");
    }


    public (TaskLog?, string?) GetLastLog(Guid userId)
    {
        try
        {
            var message = _consumer.Consume(10000);

            if (message == null) return (null, "no new messages");
            

            
            return (message.Message.Value.DeserializeTaskLog(), null);
        }
        catch (ConsumeException e)
        {
            return (null, e.Message);
        }
    }
}