using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HelloFunc.Function;

public class ServiceBusTrigger
{
    private readonly ILogger<ServiceBusTrigger> _logger;

    public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
    {
        _logger = logger;
    }

    // [Function("ServiceBusTrigger")]
    public void Run(
        [ServiceBusTrigger("myqueue", Connection = "ServiceBusConnection")] string myQueueItem)
    {
        _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
    }
}
