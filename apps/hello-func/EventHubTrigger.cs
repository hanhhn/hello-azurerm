using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HelloFunc.Function;

public class EventHubTrigger
{
    private readonly ILogger<EventHubTrigger> _logger;

    public EventHubTrigger(ILogger<EventHubTrigger> logger)
    {
        _logger = logger;
    }

    // [Function("EventHubTrigger")]
    public void Run(
        [EventHubTrigger("samples-workitems", Connection = "EventHubConnection")] string[] events)
    {
        foreach (string eventData in events)
        {
            _logger.LogInformation($"C# Event Hub trigger function processed a message: {eventData}");
        }
    }
}
