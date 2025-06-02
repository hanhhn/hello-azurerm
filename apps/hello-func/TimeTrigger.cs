using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Extensions;

namespace HelloFunc.Function;

public class TimeTrigger
{
    private readonly ILogger<TimeTrigger> _logger;

    public TimeTrigger(ILogger<TimeTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(TimeTrigger))]
    [FixedDelayRetry(5, "00:00:10")]
    public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo timerInfo,
    FunctionContext context)
    {
        var logger = context.GetLogger(nameof(TimeTrigger));
        logger.LogInformation($"Function Ran. Next timer schedule = {timerInfo.ScheduleStatus?.Next}");
    }
}
