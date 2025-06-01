using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
          Version = DefaultOpenApiConfigurationOptions.GetOpenApiDocVersion(),
          Title = $"{DefaultOpenApiConfigurationOptions.GetOpenApiDocTitle()} (Injected)",
          Description = DefaultOpenApiConfigurationOptions.GetOpenApiDocDescription(),
          TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
          Contact = new OpenApiContact()
          {
            Name = "Enquiry",
            Email = "azfunc-openapi@microsoft.com",
            Url = new Uri("https://github.com/Azure/azure-functions-openapi-extension/issues"),
          },
          License = new OpenApiLicense()
          {
            Name = "MIT",
            Url = new Uri("http://opensource.org/licenses/MIT"),
          }
        },
        Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
        OpenApiVersion = DefaultOpenApiConfigurationOptions.GetOpenApiVersion(),
        IncludeRequestingHostName = DefaultOpenApiConfigurationOptions.IsFunctionsRuntimeEnvironmentDevelopment(),
        ForceHttps = DefaultOpenApiConfigurationOptions.IsHttpsForced(),
        ForceHttp = DefaultOpenApiConfigurationOptions.IsHttpForced(),
      };

      return options;
    });

builder.Build().Run();
