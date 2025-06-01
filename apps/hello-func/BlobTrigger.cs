using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;

namespace HelloFunc.Function;

public class BlobTrigger
{
    private readonly ILogger<BlobTrigger> _logger;

    public BlobTrigger(ILogger<BlobTrigger> logger)
    {
        _logger = logger;
    }

    // [Function("BlobTrigger")]
    public void Run(
        [BlobTrigger("samples-workitems/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name)
    {
        _logger.LogInformation($"C# Blob trigger function processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    }
}
