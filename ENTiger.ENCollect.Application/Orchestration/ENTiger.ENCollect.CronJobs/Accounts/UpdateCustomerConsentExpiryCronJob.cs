using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using Cronos;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect.AccountsModule
{
    public class UpdateCustomerConsentExpiryCronJob : BackgroundService, IFlexCronJob
    {
        ///_cronSettings.CustomerConsentExpiry = "0 8 * * *"; // 08h00 every day                      
        /// ┌───────────── minute                0-59              * , - /                      
        /// │ ┌───────────── hour                0-23              * , - /                      
        /// │ │ ┌───────────── day of month      1-31              * , - /                 
        /// │ │ │ ┌───────────── month           1-12 or JAN-DEC* , - /                      
        /// │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT* , - / #                 Both 0 and 7 means SUN
        /// │ │ │ │ │
        /// * * * * *
        /// https://crontab.guru/examples.html
        /// </summary>
        /// 
        private readonly ILogger<UpdateCustomerConsentExpiryCronJob> _logger;
        readonly ProcessAccountsService _processAccountsService;
        readonly IFlexHost _flexHost;
        private readonly CronExpression _cron;
        private readonly CronJobSettings _cronSettings;

        public UpdateCustomerConsentExpiryCronJob(ILogger<UpdateCustomerConsentExpiryCronJob> logger,
            ProcessAccountsService processAccountsService, IOptions<CronJobSettings> cronSettings,
            IFlexHost flexHost)
        {
            _logger = logger;

            _processAccountsService = processAccountsService;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;

            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.CustomerConsentExpiry))
            {
                _cron = CronExpression.Parse(_cronSettings.CustomerConsentExpiry);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.CustomerConsentExpiry) && !String.IsNullOrEmpty(_cronSettings.CronTenantId))
            {
                //2025-03-24: check stopping token expiry time : timeout handling (timeout exception)
                //stoppingToken is .Net handled default 30s timeout
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        //sub token to use for methods being called to implement timeout
                        var source = new CancellationTokenSource();
                        var Now = DateTimeOffset.Now;
                        var nextUtc = _cron.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                        _logger.LogInformation($"Next {nameof(UpdateCustomerConsentExpiryCronJob)} process @ {nextUtc}");
                        if (nextUtc.HasValue)
                        {
                            await Task.Delay(nextUtc.Value - Now, stoppingToken);
                            try
                            {
                                var success = await TaskWithTimeoutAndException(ProcessConsentExpiryAsync(source.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout));
                                if (success)
                                {
                                    _logger.LogInformation($"{nameof(UpdateCustomerConsentExpiryCronJob)} executed @ {DateTime.Now}");
                                }
                            }
                            catch (TimeoutException)
                            {
                                source.Cancel();
                                _logger.LogError($"{nameof(UpdateCustomerConsentExpiryCronJob)} operation has timed out");
                            }
                            finally
                            {
                                source.Dispose();
                            }
                            
                        }
                        else
                        {
                            _logger.LogError($"{nameof(UpdateCustomerConsentExpiryCronJob)} next occurence time of process empty, cron failed. Please check configuration and/or restart process");
                        }
                    }
                }                
                catch (OperationCanceledException ex)
                {

                    if (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogError($"{nameof(UpdateCustomerConsentExpiryCronJob)} operation has been cancelled");
                    }               
                    throw;
                }
            }
            else
            {
                _logger.LogError($"{nameof(UpdateCustomerConsentExpiryCronJob)} cron string not found in appsettings, please check configuration file and restart the process");
                await StopAsync(stoppingToken);
            }
        }

        private async Task<bool> ProcessConsentExpiryAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            
            CommandResult cmdResult = null;

            UpdateCustomerConsentExpiryDto dto = new UpdateCustomerConsentExpiryDto()
            {
                ExpiryDate = DateTimeOffset.Now,
            };

            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = _cronSettings.CronTenantId
            };

            try
            {
                dto.SetAppContext(hostContextInfo);

                cmdResult = await _processAccountsService.UpdateCustomerConsentExpiryAsync(dto);

                if (cmdResult.Status != Status.Success)
                {
                    _logger.LogError($"{nameof(ProcessConsentExpiryAsync)} could not publish message {dto}, {cmdResult.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProcessConsentExpiryAsync)} exception occured in publishing message {dto}");
                //throw; could be shortlived error - log and continue with the process on next iteration
                return false;
            }
            return true;
        }

        #region Timeout handling
        private static async Task<T> DelayedTimeoutExceptionTask<T>(TimeSpan delay)
        {
            await Task.Delay(delay);
            throw new TimeoutException();
        }

        private static async Task<T> TaskWithTimeoutAndException<T>(Task<T> task, TimeSpan timeout)
        {
            return await await Task.WhenAny(
                task, DelayedTimeoutExceptionTask<T>(timeout));
        }
        #endregion

    }
}
