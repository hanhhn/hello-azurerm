using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HelloFunc.Function;

public class CosmosDBTrigger
{
  private readonly ILogger<CosmosDBTrigger> _logger;

  public CosmosDBTrigger(ILogger<CosmosDBTrigger> logger)
  {
    _logger = logger;
  }

  // [Function("CosmosDBTrigger")]
  public void Run(
      [CosmosDBTrigger(
        databaseName: "ToDoItems",
        containerName: "Items",
        Connection = "CosmosDBConnection",
        LeaseContainerName = "leases",
        CreateLeaseContainerIfNotExists = true)] IReadOnlyList<dynamic> documents)
  {
    if (documents != null && documents.Count > 0)
    {
      _logger.LogInformation($"Documents modified: {documents.Count}");
      _logger.LogInformation($"First document Id: {documents[0].id}");
    }
  }
}
