using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HelloFunc.Function;

public class MyOrchestrator
{
  private readonly ILogger<MyOrchestrator> _logger;

  public MyOrchestrator(ILogger<MyOrchestrator> logger)
  {
    _logger = logger;
  }

  // Client Function (HTTP Trigger)
  [Function("StartOrchestration")]
  [OpenApiOperation(operationId: "StartOrchestration", tags: new[] { "Hello" }, Summary = "StartOrchestration", Description = "This gets the name.", Visibility = OpenApiVisibilityType.Important)]
  [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
  [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]

  public async Task<HttpResponseData> Run(
        [Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [DurableClient] DurableTaskClient client)
  {
    string instanceId = await client.ScheduleNewOrchestrationInstanceAsync("MyOrchestrator");

    _logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

    var response = req.CreateResponse(HttpStatusCode.OK);
    await response.WriteStringAsync($"Orchestration started with ID = {instanceId}");
    return response;
  }

  // Orchestrator function
  [FunctionName("MyOrchestrator")]
  public async Task RunOrchestrator(
      [OrchestrationTrigger] TaskOrchestrationContext context)
  {
    var apiResult = await context.CallActivityAsync<string>("CallApi", null);
    await context.CallActivityAsync("SaveToDatabase", apiResult);
    await context.CallActivityAsync("SendEmail", apiResult);
  }


  // Activity Function: CallApi
  [FunctionName("CallApi")]
  public Task<string> CallApi([ActivityTrigger] string input)
  {
    // Gọi API thật
    Console.WriteLine($"CallApi");
    return Task.FromResult<string>("Called data " + DateTime.UtcNow.ToShortTimeString());
  }

  // Activity Function: SaveToDatabase
  [FunctionName("SaveToDatabase")]
  public Task SaveToDatabase([ActivityTrigger] string data)
  {
    // Lưu dữ liệu vào DB
    Console.WriteLine($"Saved: {data}");
    return Task.CompletedTask;
  }

  // Activity Function: SendEmail
  [FunctionName("SendEmail")]
  public async Task SendEmail([ActivityTrigger] string data)
  {
    // Gửi email
    Console.WriteLine($"Sent email with data: {data}");
  }
}
