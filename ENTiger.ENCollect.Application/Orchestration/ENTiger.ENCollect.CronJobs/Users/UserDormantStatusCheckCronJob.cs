using Cronos;
using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CronJobs.Users
{
    public class UserDormantStatusCheckCronJob : BackgroundService, IFlexCronJob
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
        private readonly ILogger<UserDormantStatusCheckCronJob> _logger;
        readonly ProcessAgencyUsersService _processAgencyUsersService;
        readonly ProcessCompanyUsersService _processCompanyUsersService;
        private readonly CronExpression _cron;
        private readonly CronJobSettings _cronSettings;
        private readonly SystemUserSettings _userSettings;
        private readonly IRepoFactory _repoFactory;

        public UserDormantStatusCheckCronJob(ILogger<UserDormantStatusCheckCronJob> logger, IRepoFactory repoFactory, 
            ProcessAgencyUsersService processAgencyUsersService, ProcessCompanyUsersService processCompanyUsersService, IOptions<CronJobSettings> cronSettings, IOptions<SystemUserSettings> userSettings 
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _processAgencyUsersService = processAgencyUsersService;
            _processCompanyUsersService = processCompanyUsersService;
            _cronSettings = cronSettings.Value;
            _userSettings = userSettings.Value;

            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.UserDormantStatusCheck))
            {
                _cron = CronExpression.Parse(_cronSettings.UserDormantStatusCheck);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.UserDormantStatusCheck) && !String.IsNullOrEmpty(_cronSettings.CronTenantId))
            {
                //stopping token expiry time : timeout handling (timeout exception)
                //stoppingToken is .Net handled default 30s timeout
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        //sub token to use for methods being called to implement timeout
                        var source = new CancellationTokenSource();
                        var Now = DateTimeOffset.Now;
                        var nextUtc = _cron.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                        _logger.LogInformation($"Next {nameof(UserDormantStatusCheckCronJob)} process @ {nextUtc}");
                        if (nextUtc.HasValue)
                        {
                            await Task.Delay(nextUtc.Value - Now, stoppingToken);
                            try
                            {
                                var task1 = await TaskWithTimeoutAndException(ProcessCompanyUserActivityCheckAsync(source.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout), "ProcessCompanyUserActivityCheckAsync");                          
                                if (task1)
                                {
                                    _logger.LogInformation($"{nameof(UserDormantStatusCheckCronJob)} - ProcessCompanyUserActivityCheckAsync executed @ {DateTime.Now}");
                                }
                                var task2 = await TaskWithTimeoutAndException(ProcessAgencyUserActivityCheckAsync(source.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout), "ProcessAgencyUserActivityCheckAsync");
                                if (task2)
                                {
                                    _logger.LogInformation($"{nameof(UserDormantStatusCheckCronJob)} - ProcessAgencyUserActivityCheckAsync executed @ {DateTime.Now}");
                                }
                            }
                            catch (TimeoutException tex)
                            {
                                source.Cancel();
                                _logger.LogError($"{nameof(UserDormantStatusCheckCronJob)} operation has timed out | {tex.Message}");
                            }
                            finally
                            {
                                source.Dispose();
                            }
                        }
                        else
                        {
                            _logger.LogError($"{nameof(UserDormantStatusCheckCronJob)} next occurence time of process empty, cron failed. Please check configuration and/or restart process");
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {

                    if (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogError($"{nameof(UserDormantStatusCheckCronJob)} operation has been cancelled");
                    }
                    throw;
                }
            }
            else
            {
                _logger.LogError($"{nameof(UserDormantStatusCheckCronJob)} cron string not found in appsettings, please check configuration file and restart the process");
                await StopAsync(stoppingToken);
            }
        }

        private async Task<bool> ProcessCompanyUserActivityCheckAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = _cronSettings.CronTenantId,
                UserId = _userSettings.SystemUserId,
                ClientIP = "",
                RequestSource = TransactionSourceEnum.System.Value
            };

            CommandResult cmdCompanyResult = null;
            DormantCompanyUserDto companyDto = new();
            companyDto.companyUserIds = new List<string>();

            try
            {
                companyDto.SetAppContext(hostContextInfo);
                _repoFactory.Init(companyDto);

                var companyUserIds = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                    .IncludeCompanyUserWorkflow()
                    .IncludeCompanyUserAttendanceLog()
                    .ByCompanyUserInactivity(_userSettings.UserInactivityDormantDays)
                    .Select(s=>s.Id)
                    .ToListAsync(stoppingToken);
                
                if (companyUserIds is not null && companyUserIds.Count != 0)
                {
                    companyDto.companyUserIds = companyUserIds;
                    cmdCompanyResult = await _processCompanyUsersService.MakeDormantCompanyUser(companyDto);

                    if (cmdCompanyResult?.Status != Status.Success)
                    {
                        _logger.LogError($"{nameof(ProcessCompanyUserActivityCheckAsync)} could not publish dormant company users message {companyDto}, {cmdCompanyResult?.Message ?? ""}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProcessCompanyUserActivityCheckAsync)} exception occured in publishing dormant company users message");
                //throw; could be shortlived error - log and continue with the process on next iteration
                return false;
            }
            return true;
        }

        private async Task<bool> ProcessAgencyUserActivityCheckAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = _cronSettings.CronTenantId,
                UserId = _userSettings.SystemUserId,
                ClientIP = "",
                RequestSource = TransactionSourceEnum.System.Value
            };

            CommandResult cmdAgencyResult = null;
            DormantAgencyUserDto agencyDto = new();
            agencyDto.AgentIds = new List<string>();

            try
            {
                agencyDto.SetAppContext(hostContextInfo);
                _repoFactory.Init(agencyDto);

                var agencyUserIds = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                    .IncludeAgencyUserWorkflow()
                    .IncludeAgencyUserAttendanceLog()
                    .ByAgencyUserInactivity(_userSettings.UserInactivityDormantDays)
                    .Select(s => s.Id)
                    .ToListAsync(stoppingToken);

                if (agencyUserIds is not null && agencyUserIds.Count != 0)
                {
                    agencyDto.AgentIds = agencyUserIds;
                    cmdAgencyResult = await _processAgencyUsersService.MakeDormantAgencyUser(agencyDto);

                    if (cmdAgencyResult?.Status != Status.Success)
                    {
                        _logger.LogError($"{nameof(ProcessAgencyUserActivityCheckAsync)} could not publish dormant agency users message {agencyDto}, {cmdAgencyResult?.Message ?? ""}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProcessAgencyUserActivityCheckAsync)} exception occured in publishing dormant agency users message");
                //throw; could be shortlived error - log and continue with the process on next iteration
                return false;
            }
            return true;
        }

        #region Timeout handling
        private static async Task<T> DelayedTimeoutExceptionTask<T>(TimeSpan delay, string message = "")
        {
            await Task.Delay(delay);
            throw new TimeoutException(message);
        }

        private static async Task<T> TaskWithTimeoutAndException<T>(Task<T> task, TimeSpan timeout, string message = "")
        {
            return await await Task.WhenAny(
                task, DelayedTimeoutExceptionTask<T>(timeout, message));
        }
        #endregion
    }
}
