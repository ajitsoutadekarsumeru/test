using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class RunTriggersCronJob : BackgroundService, IFlexCronJob
    {
        private readonly ILogger<RunTriggersCronJob> _logger;
        readonly ProcessCommunicationService _processCommunicationService;
        readonly IFlexHost _flexHost;
        private readonly CronJobSettings _cronSettings;

        public RunTriggersCronJob(ILogger<RunTriggersCronJob> logger,
            ProcessCommunicationService processCommunicationService, IOptions<CronJobSettings> cronSettings,
            IFlexHost flexHost)
        {
            _logger = logger;

            _processCommunicationService = processCommunicationService;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CommandResult cmdResult = null;

            RunTriggersDto dto = new RunTriggersDto()
            {
                //read external data and set value for the model

            };


            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                //Populate the context info here. 
                TenantId = _cronSettings.CronTenantId

            };
           

            dto.SetAppContext(hostContextInfo);

            cmdResult = await _processCommunicationService.RunTriggers(dto);

            if (cmdResult.Status != Status.Success)
            {
                //If validation fails, handle error here

            }

            await Task.CompletedTask;
        }
    }
}
