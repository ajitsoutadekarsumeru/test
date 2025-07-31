using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    public class SmsUtility : ISmsUtility
    {
        protected readonly ILogger<SmsUtility> _logger;
        protected IFlexHost _flexHost;
        protected IRepoFactory _repoFactory;
        protected string? serviceProvider;
        private readonly SmsProviderFactory _smsProviderFactory;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        protected string? countryCode;

        public SmsUtility(ILogger<SmsUtility> logger, IRepoFactory repoFactory,
            SmsProviderFactory smsProviderFactory, IFlexHost flexHost)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsProviderFactory = smsProviderFactory;
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _flexHost = flexHost;
        }

        public async Task<bool> SendSMS(string numbers, string message, string tenantId, string? language = null)
        {
            _logger.LogInformation("SmsUtility : SendSMS - Start | TenantId - " + tenantId);
            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = tenantId
            };
            _repoFactory.Init(hostContextInfo);
            bool result = false;

            List<FeatureMaster> features = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                        .Where(x => string.Equals(x.Parameter, "smsserviceprovider") || string.Equals(x.Parameter, "countrycode"))
                                        .ToListAsync();

            if (features != null && features.Count > 0)
            {
                serviceProvider = features.Where(x => string.Equals(x.Parameter, "smsserviceprovider")).Select(x => x.Value).FirstOrDefault();
                countryCode = features.Where(x => string.Equals(x.Parameter, "countrycode")).Select(x => x.Value).FirstOrDefault();
            }
            //serviceProvider = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
            //                            .Where(x => string.Equals(x.Parameter, "smsserviceprovider"))
            //                            .Select(x => x.Value).FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(serviceProvider))
            {
                _logger.LogError("Please configure the <<SMSServiceProvider>> in DB | FeatureMaster Table | TenantId - " + tenantId);
            }
            else
            {
                List<TenantSMSConfiguration> model = new List<TenantSMSConfiguration>();

                model = await _repoTenantFactory.FindAll<TenantSMSConfiguration>().Where(x => x.TenantId == tenantId).ToListAsync();
                if (model == null || model.Count <= 0)
                {
                    _logger.LogError("Please configure the SMS Configuration in Tenant DB | TenantId - " + tenantId);
                }
                else
                {
                    var smsLogPath = model.Where(x => string.Equals(x.Key, "logpath")).Select(a => a.Value).FirstOrDefault();

                    string? hostName = await _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId).Select(x => x.HostName).FirstOrDefaultAsync();

                    if (string.IsNullOrEmpty(smsLogPath))
                    {
                        _logger.LogError("SmsUtility : Please configure the <<SmsLogPath>> in Tenant DB | TenantSMSConfiguration Table");
                    }
                    else
                    {
                        string logFilePath = Path.Combine(smsLogPath, tenantId);

                        if (!Directory.Exists(logFilePath))
                        {
                            Directory.CreateDirectory(logFilePath);
                        }

                        string logname = "SMSLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                        string file = Path.Combine(logFilePath, logname);
                        _logger.LogInformation("SmsUtility : HostName - " + hostName + " | SMS Provider - " + serviceProvider);

                        //Get the SMS provider
                        //var smsProvider = _smsProviderFactory.GetSmsProvider(serviceProvider);
                        var utility = _flexHost.GetUtilityService<SmsProviderFactory>(hostName);
                        var smsProvider = utility.GetSmsProvider(serviceProvider);

                        //Call SMS provider
                        var fullNumber = $"{countryCode}{numbers}";
                        _logger.LogDebug("MobileNumberwithcountryCode - " + fullNumber);
                        result = await smsProvider.SendSmsAsync(model, fullNumber, message, file);

                        //result = await smsProvider.SendSmsAsync(model, numbers, message, file);
                    }
                }
            }
            _logger.LogInformation("SmsUtility : SendSMS - End");
            return result;
        }
    }
}