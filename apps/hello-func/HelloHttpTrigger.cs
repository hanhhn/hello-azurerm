using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using Microsoft.OpenApi.Models;

namespace HelloFunc.Function;

public class HelloHttpTrigger
{
  private readonly ILogger<HelloHttpTrigger> _logger;

  public HelloHttpTrigger(ILogger<HelloHttpTrigger> logger)
  {
    _logger = logger;
  }

  [Function("HelloHttpTrigger")]
  [OpenApiOperation(operationId: "Run", tags: new[] { "Hello" })]
  [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
  [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
  public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    return new OkObjectResult("Welcome to Azure Functions!");
  }

  // Add these four attribute classes below
  [Function("Test")]
  [OpenApiOperation(operationId: "test", tags: new[] { "name" }, Summary = "Gets the name", Description = "This gets the name.", Visibility = OpenApiVisibilityType.Important)]
  [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
  [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name", Description = "The name", Visibility = OpenApiVisibilityType.Important)]
  [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]
  public IActionResult Test([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    return new OkObjectResult("Welcome to Azure Functions!");
  }
}
