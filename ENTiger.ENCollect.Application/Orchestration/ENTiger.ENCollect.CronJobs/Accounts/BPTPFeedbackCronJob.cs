using Cronos;
using ENCollect.Security;
using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.AccountsModule
{
    public class BPTPFeedbackCronJob : BackgroundService, IFlexCronJob
    {
        ///_cronSettings.BPTPFeedbackCron = "0 23 * * *"; // 23h00 every day                      
        /// ┌───────────── minute                0-59              * , - /                      
        /// │ ┌───────────── hour                0-23              * , - /                      
        /// │ │ ┌───────────── day of month      1-31              * , - /                 
        /// │ │ │ ┌───────────── month           1-12 or JAN-DEC* , - /                      
        /// │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT* , - / #                 Both 0 and 7 means SUN
        /// │ │ │ │ │
        /// * * * * *
        private readonly ILogger<BPTPFeedbackCronJob> _logger;
        readonly ProcessFeedbackService _processFeedbackService;
        readonly IFlexHost _flexHost;
        private readonly CronExpression _cron;
        private readonly CronJobSettings _cronSettings;
        private readonly EncryptionSettings _encryptionSettings;
        private readonly SystemUserSettings _systemUserSettings;
        private readonly AesGcmCrypto _aesGcmCrypto;
        protected readonly IRepoFactory _repoFactory;

        public BPTPFeedbackCronJob(ILogger<BPTPFeedbackCronJob> logger, IRepoFactory repoFactory,
            ProcessFeedbackService processFeedbackService, IOptions<CronJobSettings> cronSettings,
            IFlexHost flexHost, AesGcmCrypto aesGcmCrypto, IOptions<EncryptionSettings> encryptionSettings, IOptions<SystemUserSettings> systemUserSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _encryptionSettings = encryptionSettings.Value;
            _systemUserSettings = systemUserSettings.Value;
            _aesGcmCrypto = aesGcmCrypto;
            _processFeedbackService = processFeedbackService;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;

            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.BPTPFeedbackCron))
            {
                _cron = CronExpression.Parse(_cronSettings.BPTPFeedbackCron);
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.BPTPFeedbackCron) && !String.IsNullOrEmpty(_cronSettings.CronTenantId))
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
                        _logger.LogInformation($"Next {nameof(BPTPFeedbackCronJob)} process @ {nextUtc}");
                        if (nextUtc.HasValue)
                        {
                            await Task.Delay(nextUtc.Value - Now, stoppingToken);
                            try
                            {
                                var success = await TaskWithTimeoutAndException(ProcessBPTPFeedbackAsync(source.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout));
                                if (success)
                                {
                                    _logger.LogInformation($"{nameof(BPTPFeedbackCronJob)} executed @ {DateTime.Now}");
                                }
                            }
                            catch (TimeoutException)
                            {
                                source.Cancel();
                                _logger.LogError($"{nameof(BPTPFeedbackCronJob)} operation has timed out");
                            }
                            finally
                            {
                                source.Dispose();
                            }

                        }
                        else
                        {
                            _logger.LogError($"{nameof(BPTPFeedbackCronJob)} next occurence time of process empty, cron failed. Please check configuration and/or restart process");
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {

                    if (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogError($"{nameof(BPTPFeedbackCronJob)} operation has been cancelled");
                    }
                    throw;
                }
            }
            else
            {
                _logger.LogError($"{nameof(BPTPFeedbackCronJob)} cron string not found in appsettings, please check configuration file and restart the process");
                await StopAsync(stoppingToken);
            }
        }

        private async Task<bool> ProcessBPTPFeedbackAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            CommandResult cmdResult = null;
            FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
            {
                TenantId = _cronSettings.CronTenantId
            };
            AddFeedbackDto dto = new();
            try
            {
                string key = _encryptionSettings.StaticKeys.DecryptionKey;
                //get the system user id
                hostContextInfo.UserId = _systemUserSettings.SystemUserId;
                hostContextInfo.RequestSource = TransactionSourceEnum.System.Value;

                var aesGcmKey = Encoding.UTF8.GetBytes(key);
                
                dto.SetAppContext(hostContextInfo);

                _repoFactory.Init(dto);

                //get affected accounts: LatestPTPDate = today, LatestPaymentDate < today
                List<LoanAccount> accountlist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                        .ByPrevDayPTPAndNoPayment()
                                                        .ToListAsync();
                _logger.LogInformation($"Number of accounts that meet the BPTP criteria: {accountlist.Count}");
                foreach (var acc in accountlist)
                {
                    dto.Accountno = _aesGcmCrypto.Encrypt(acc.AGREEMENTID, aesGcmKey);
                    dto.DispositionDate = DateTime.Today;
                    dto.PTPDate = acc.LatestPTPDate;
                    dto.PTPAmount = acc.LatestPTPAmount;
                    dto.CustomerMet = "No";
                    dto.DispositionCode = "BPTP";
                    dto.DispositionGroup = "BPTP";
                    dto.Remarks = "Non payment";
                    var command = new AddFeedbackCommand { Dto = dto };
                    cmdResult = await _processFeedbackService.AddFeedback(dto);
                    if (cmdResult.Status != Status.Success)
                    {
                        _logger.LogError($"{nameof(ProcessBPTPFeedbackAsync)} could not send add feedback command {dto}, {cmdResult.Message}");
                    }
                    else
                    {
                        _logger.LogInformation($"Added feedback with DispositionCode 'BPTP' to Account No: {acc.AGREEMENTID}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProcessBPTPFeedbackAsync)} exception occured sending add feedback command");
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
