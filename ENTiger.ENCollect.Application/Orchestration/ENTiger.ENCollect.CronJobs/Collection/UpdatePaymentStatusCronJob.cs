using Cronos;
using ENTiger.ENCollect.CollectionsModule;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CronJobs.Collection
{
    /// <summary>
    /// Background service that runs on a cron schedule to update payment status for a tenant.
    /// </summary>
    public class UpdatePaymentStatusCronJob : BackgroundService, IFlexCronJob
    {
        private readonly ILogger<UpdatePaymentStatusCronJob> _logger;
        private readonly IRepoFactory _repoFactory;
        private readonly CronExpression? _cronExpression;
        private readonly CronJobSettings _cronSettings;
        private readonly string _tenantId;
        private readonly IFlexServiceBusBridge _bus;
        private readonly ICollectionRepository _collectionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentStatusCronJob"/> class.
        /// </summary>
        public UpdatePaymentStatusCronJob(
            ILogger<UpdatePaymentStatusCronJob> logger,
            IRepoFactory repoFactory,
            IOptions<CronJobSettings> cronSettings,
            IOptions<CannedReportSetting> cannedReportSettings,
            IFlexServiceBusBridge bus,
            ICollectionRepository collectionRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _cronSettings = cronSettings.Value;
            _tenantId = cannedReportSettings.Value.TenantId;
            _bus = bus;
            _collectionRepository = collectionRepository;

            if (!string.IsNullOrWhiteSpace(_cronSettings.PaymentStatusCron))
            {
                try
                {
                    _cronExpression = CronExpression.Parse(_cronSettings.PaymentStatusCron);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Invalid cron expression: {Cron}", _cronSettings.PaymentStatusCron);
                }
            }
            else
            {
                _logger.LogWarning("UpdatePaymentStatusCronJob: No cron expression configured.");
            }
        }

        /// <summary>
        /// Starts the background service and executes the scheduled task in a loop based on the cron expression.
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronExpression is null)
            {
                _logger.LogWarning("UpdatePaymentStatusCronJob not started due to invalid cron expression.");
                return;
            }

            _logger.LogInformation("UpdatePaymentStatusCronJob started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = GetNextDelay();
                if (delay <= TimeSpan.Zero)
                {
                    _logger.LogWarning("UpdatePaymentStatusCronJob: Unable to determine next valid execution time.");
                    break;
                }

                _logger.LogInformation("UpdatePaymentStatusCronJob: Waiting {Delay} for next execution.", delay);
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                await ProcessPaymentStatusUpdateAsync(stoppingToken);
            }

            _logger.LogInformation("UpdatePaymentStatusCronJob stopped.");
        }

        /// <summary>
        /// Calculates the delay until the next scheduled execution time based on the cron expression.
        /// </summary>
        private TimeSpan GetNextDelay()
        {
            var nextUtc = _cronExpression?.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
            return nextUtc.HasValue ? nextUtc.Value - DateTimeOffset.UtcNow : TimeSpan.Zero;
        }

        /// <summary>
        /// Executes the core logic for updating payment statuses by sending commands to the service bus.
        /// </summary>
        private async Task ProcessPaymentStatusUpdateAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("UpdatePaymentStatusCronJob: Triggering payment status update for T-1 date.");

                var context = new FlexAppContextBridge { TenantId = _tenantId };
                var inputDate = DateTime.Today.AddDays(-1);

                var collectionIds = await _collectionRepository.GetOnlineCollectionIdsByDateAsync(inputDate, context);

                if (collectionIds.Any())
                {
                    _logger.LogInformation("UpdatePaymentStatusCronJob: Found {Count} collections to update.", collectionIds.Count);

                    foreach (var collectionId in collectionIds)
                    {
                        var dto = new UpdatePaymentStatusDto { CollectionId = collectionId };
                        dto.SetAppContext(context);

                        var cmd = new UpdatePaymentStatusCommand { Dto = dto };
                        await _bus.Send(cmd);
                    }
                }
                else
                {
                    _logger.LogWarning("UpdatePaymentStatusCronJob: No online collections found for {Date}.", inputDate.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdatePaymentStatusCronJob: Exception occurred during execution.");
            }
        }
    }
}
