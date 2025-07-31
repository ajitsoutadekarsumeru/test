using ENTiger.ENCollect.AccountsModule;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateGeoLocationsCronJob : BackgroundService, IFlexCronJob
    {
        private readonly ILogger<UpdateGeoLocationsCronJob> _logger;
        readonly ProcessGeoLocationService _processGeoLocationService;
        readonly IFlexHost _flexHost;
        private readonly CronJobSettings _cronSettings;

        public UpdateGeoLocationsCronJob(ILogger<UpdateGeoLocationsCronJob> logger,ProcessGeoLocationService processGeoLocationService,
                IFlexHost flexHost, IOptions<CronJobSettings> cronSettings) 
        {
            _logger = logger;
            _processGeoLocationService = processGeoLocationService;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.CronTenantId))
            {
                CommandResult cmdResult = null;

                UpdateGeoLocationsDto dto = new UpdateGeoLocationsDto()
                {
                    //read external data and set value for the model
                };

                FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
                {
                    //Populate the context info here. 
                    TenantId = _cronSettings.CronTenantId
                };

                dto.SetAppContext(hostContextInfo);
                _logger.LogDebug("UpdateGeoLocationsCronJob Tenantid :" + _cronSettings.CronTenantId);
                cmdResult = await _processGeoLocationService.UpdateGeoLocations(dto);

                if (cmdResult.Status != Status.Success)
                {
                    //If validation fails, handle error here
                }
            }
            else
            {
                _logger.LogError($"{nameof(UpdateGeoLocationsCronJob)} cron string not found in appsettings, please check configuration file and restart the process");
            }
        }
    }
}
