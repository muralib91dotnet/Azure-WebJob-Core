using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWebJob.DI.Services;
using SampleWebJob.DI.WebJobConfiguration;
using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleWebJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddEnvironmentVariables(prefix: "ASPNETCORE_")
            //.AddJsonFile($"appsettings.json", true)
            ////.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true)
            //.AddCommandLine(args)
            //.Build();

            IHost host = new HostBuilder()
             .ConfigureHostConfiguration(configHost =>
             {
                 configHost.SetBasePath(Directory.GetCurrentDirectory());
                 configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                 configHost.AddCommandLine(args);
             })
             .ConfigureAppConfiguration((hostContext, configApp) =>
             {
                 configApp.SetBasePath(Directory.GetCurrentDirectory());
                 configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                 configApp.AddJsonFile($"appsettings.json", true);
                 configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                 configApp.AddCommandLine(args);
             })
            .ConfigureServices((hostContext, services) =>
            {
 
                services.AddLogging();
                services.AddHostedService<ApplicationHostService>();
                services.AddScoped<ITestService, TestService>();
                //services.Configure<WebJobConfiguration>(hostContext.Configuration.GetSection("WebJobConfiguration"));
                //services.AddSingleton<IWebJobConfiguration>(hostContext.Configuration.GetSection(nameof(WebJobConfiguration)).Get<WebJobConfiguration>());

            })
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddSerilog(new LoggerConfiguration()
                          .ReadFrom.Configuration(hostContext.Configuration)
                          .CreateLogger());
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .Build();

            try
            {
                await host.RunAsync();
            }
            catch (HostingStopException) {
                //Host terminated
            }
        }
    }
}
