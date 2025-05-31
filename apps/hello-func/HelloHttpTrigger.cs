using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HelloFunc.Function;

public class HelloHttpTrigger
{
    private readonly ILogger<HelloHttpTrigger> _logger;

    public HelloHttpTrigger(ILogger<HelloHttpTrigger> logger)
    {
        _logger = logger;
    }

    [Function("HelloHttpTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
