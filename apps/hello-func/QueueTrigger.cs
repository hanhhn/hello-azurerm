using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HelloFunc.Function;

public class QueueTrigger
{
    private readonly ILogger<QueueTrigger> _logger;

    public QueueTrigger(ILogger<QueueTrigger> logger)
    {
        _logger = logger;
    }

    // [Function("QueueTrigger")]
    public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] string myQueueItem)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    }
}
