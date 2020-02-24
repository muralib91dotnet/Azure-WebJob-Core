using Microsoft.Azure.WebJobs.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWebJob.DI.Services;
using SampleWebJob.DI.WebJobConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWebJob
{
    public class ApplicationHostService : IHostedService
    {
        readonly ILogger<ApplicationHostService> _logger;
        readonly IConfiguration _configuration;
        //readonly IWebJobConfiguration _webJobConfiguration;
        readonly ITestService _testService;
        readonly IHostingEnvironment _hostingEnvironment;
        public ApplicationHostService(
            ILogger<ApplicationHostService> logger,
            IConfiguration configuration,
            //IWebJobConfiguration webJobConfiguration,
            IHostingEnvironment hostingEnvironment,
            ITestService testService
            )
        {
            _logger = logger;
            _configuration = configuration;
            //_webJobConfiguration = webJobConfiguration;
            _hostingEnvironment = hostingEnvironment;
            _testService = testService;
        }


        //public async Task ProcessQueueMessageAsync([QueueTrigger("EmailMessageQueue")] string message)
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //_logger.LogInformation(_webJobConfiguration.Message);
            _logger.LogInformation(_testService.GetType().ToString());

            //Do something
            _logger.LogWarning("Hello from Sample Webjob");

            //Throw exception to terminate the host
            throw new HostingStopException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
