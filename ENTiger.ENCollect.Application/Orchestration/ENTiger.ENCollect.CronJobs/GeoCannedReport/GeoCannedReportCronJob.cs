using Cronos;
using ENTiger.ENCollect.GeoTagModule;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CronJobs.GeoCannedReport;

/// <summary>
/// Cron job that triggers the Geo Canned Report generation on a scheduled basis using the configured cron expression.
/// </summary>
public sealed class GeoCannedReportCronJob : BackgroundService, IFlexCronJob
{
    private readonly ILogger<GeoCannedReportCronJob> _logger;
    private readonly IRepoFactory _repoFactory;
    private readonly CronExpression? _cronExpression;
    private readonly CronJobSettings _cronSettings;
    private readonly ProcessGeoTagService _processGeoTagService;
    private readonly CannedReportSetting _cannedReportSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="GeoCannedReportCronJob"/> class.
    /// </summary>
    /// <param name="logger">Logger instance for writing logs.</param>
    /// <param name="repoFactory">Repository factory for accessing persistence services.</param>
    /// <param name="cronSettings">Cron schedule configuration options.</param>
    /// <param name="processGeoTagService">Service for processing geo-tag report generation.</param>
    /// <param name="cannedReportSettings">Settings specific to canned reports.</param>
    public GeoCannedReportCronJob(
        ILogger<GeoCannedReportCronJob> logger,
        IRepoFactory repoFactory,
        IOptions<CronJobSettings> cronSettings,
        ProcessGeoTagService processGeoTagService,
        IOptions<CannedReportSetting> cannedReportSettings)
    {
        _logger = logger;
        _repoFactory = repoFactory;
        _processGeoTagService = processGeoTagService;
        _cronSettings = cronSettings.Value;
        _cannedReportSettings = cannedReportSettings.Value;

        if (!string.IsNullOrWhiteSpace(_cronSettings.GeoScheduleCron))
        {
            try
            {
                _cronExpression = CronExpression.Parse(_cronSettings.GeoScheduleCron);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid cron expression: {Cron}", _cronSettings.GeoScheduleCron);
            }
        }
        else
        {
            _logger.LogWarning("GeoCannedReportCronJob: No cron expression configured.");
        }
    }

    /// <summary>
    /// Starts the background service and runs the cron-based execution loop.
    /// </summary>
    /// <param name="stoppingToken">Token to observe for cancellation requests.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_cronExpression is null)
        {
            _logger.LogWarning("GeoCannedReportCronJob not started due to invalid cron expression.");
            return;
        }

        _logger.LogInformation("GeoCannedReportCronJob started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var delay = GetNextDelay();
            if (delay <= TimeSpan.Zero)
            {
                _logger.LogWarning("GeoCannedReportCronJob: Unable to determine next valid execution time.");
                break;
            }

            _logger.LogInformation("GeoCannedReportCronJob: Waiting {Delay} for next execution.", delay);
            try
            {
               await Task.Delay(delay, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }

            await GenerateGeoCannedReportAsync(stoppingToken);
        }

        _logger.LogInformation("GeoCannedReportCronJob stopped.");
    }

    /// <summary>
    /// Calculates the delay duration until the next occurrence based on the configured cron expression.
    /// </summary>
    /// <returns>TimeSpan representing the delay duration.</returns>
    private TimeSpan GetNextDelay()
    {
        var nextUtc = _cronExpression?.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
        return nextUtc.HasValue ? nextUtc.Value - DateTimeOffset.UtcNow : TimeSpan.Zero;
    }

    /// <summary>
    /// Invokes the geo-tag report generation service and logs the result.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token passed from the cron loop.</param>
    private async Task GenerateGeoCannedReportAsync(CancellationToken stoppingToken)
    {
        var reportDate = DateTime.Today.AddDays(-1);

        var context = new FlexAppContextBridge
        {
            TenantId = _cannedReportSettings.TenantId
        };

        var dto = new GeoCannedReportDto
        {
            reportDate = reportDate
        };

        dto.SetAppContext(context);

        try
        {
            _logger.LogInformation("GeoCannedReportCronJob: Triggering Geo report generation for date: {Date}", reportDate);

            var cmdResult = await _processGeoTagService.GenerateGeoReport(dto);

            if (cmdResult.Status != Status.Success)
            {
                _logger.LogError("GeoCannedReportCronJob: Failed to generate report. Reason: {Message}", cmdResult.Message);
                return;
            }

            _logger.LogInformation("GeoCannedReportCronJob: Report executed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GeoCannedReportCronJob: Exception occurred during report generation for date: {Date}", reportDate);
        }
    }
}
