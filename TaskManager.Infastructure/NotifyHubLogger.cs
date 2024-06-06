using System.Net;
using Confluent.Kafka;
using TaskManager.Core;
using TaskManager.Infastructure.Extentions;

namespace TaskManager.Infastructure;

public class NotifyHubLogger : INotifyHubLogger
{
    private readonly ProducerConfig _config = new()
    {
        BootstrapServers = "localhost:29092",
        ClientId = Dns.GetHostName()
    };

    private readonly IProducer<Null, string> _producer;


    public NotifyHubLogger()
    {
        _producer = new ProducerBuilder<Null, string>(_config).Build();
    }

    public async Task<string?> Log(TaskLog taskLog)
    {
        try
        {
            await _producer.ProduceAsync("UserTasks", new Message<Null, string>
            {
                Value = taskLog.Serilize()
            });
            return null;
        }
        catch (ProduceException<Null, string> e)
        {
            throw e;
            return e.Message;
        }
    }
}