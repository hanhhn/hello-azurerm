using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .AddSingleton<IOpenApiConfigurationOptions>(_ =>
    {
        var options = new OpenApiConfigurationOptions()
        {
            Info = new OpenApiInfo()
            {
                Version = "1.0.0",
                Title = "Hello Function API",
                Description = "API documentation for Hello Function"
            },
            Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
            OpenApiVersion = OpenApiVersionType.V3,
            IncludeRequestingHostName = true,
            ForceHttps = false,
            ForceHttp = false,
        };

        return options;
    });

builder.Build().Run();
