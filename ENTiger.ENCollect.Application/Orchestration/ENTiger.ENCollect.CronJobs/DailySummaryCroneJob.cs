using Cronos;
using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.DomainModels.Utilities;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;
using System.IO.Compression;
using System.Reflection;

namespace ENTiger.ENCollect.CronJobs
{
    public class DailySummaryCroneJob : BackgroundService, IFlexCronJob
    {
        ///_cronSettings.DailySummaryCroneJob = "0 23 * * *"; // 23h00 every day                      
        /// ┌───────────── minute                0-59              * , - /                      
        /// │ ┌───────────── hour                0-23              * , - /                      
        /// │ │ ┌───────────── day of month      1-31              * , - /                 
        /// │ │ │ ┌───────────── month           1-12 or JAN-DEC* , - /                      
        /// │ │ │ │ ┌───────────── day of week   0-6  or SUN-SAT* , - / #                 Both 0 and 7 means SUN
        /// │ │ │ │ │
        /// * * * * *
        private readonly ILogger<DailySummaryCroneJob> _logger;
        readonly IFlexHost _flexHost;
        private readonly CronExpression _cron;
        private readonly CronJobSettings _cronSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly SystemUserSettings _systemUserSettings;
        protected readonly IRepoFactory _repoFactory;
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _fileSettings;
        private readonly CannedReportSetting _cannedReportsettings;
        private readonly IDistanceCalculatorService _distanceCalculatorService;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly IFileValidationUtility _fileValidationUtility;
        private readonly IEmailUtility _emailUtility;
        private readonly FileValidationSettings _settings;

        public DailySummaryCroneJob(ILogger<DailySummaryCroneJob> logger, IRepoFactory repoFactory, IServiceProvider serviceProvider,
            IOptions<CronJobSettings> cronSettings, IOptions<DatabaseSettings> databaseSettings,
            IFlexHost flexHost,
            IOptions<SystemUserSettings> systemUserSettings,
            IFileSystem fileSystem,
           IOptions<FilePathSettings> fileSettings,
           IOptions<CannedReportSetting> cannedReportsettings, IDistanceCalculatorService distanceCalculatorService,
           IOptions<FileConfigurationSettings> fileConfigurationSettings,ICsvExcelUtility csvExcelUtility, IFileValidationUtility fileValidationUtility,
            IEmailUtility emailUtility, IOptions<FileValidationSettings> settings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _repoTenantFactory = serviceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _systemUserSettings = systemUserSettings.Value;
            _flexHost = flexHost;
            _cronSettings = cronSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _fileSystem = fileSystem;
            _fileSettings = fileSettings.Value;
            _cannedReportsettings = cannedReportsettings.Value;

            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.DailySummaryCron))
            {
                _cron = CronExpression.Parse(_cronSettings.DailySummaryCron);
            }

            _distanceCalculatorService = distanceCalculatorService;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _csvExcelUtility = csvExcelUtility;
            _fileValidationUtility = fileValidationUtility;
            _emailUtility = emailUtility;
            _settings = settings.Value;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_cronSettings != null && !String.IsNullOrEmpty(_cronSettings.DailySummaryCron) && !String.IsNullOrEmpty(_cronSettings.CronTenantId))
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
                        _logger.LogInformation($"Next {nameof(DailySummaryCroneJob)} process @ {nextUtc}");
                        if (nextUtc.HasValue)
                        {
                            await Task.Delay(nextUtc.Value - Now, stoppingToken);
                            try
                            {
                                var success = await TaskWithTimeoutAndException(ProcessDailySummaryAsync(source.Token), TimeSpan.FromSeconds(_cronSettings.CronTimeout));
                                if (success)
                                {
                                    _logger.LogInformation($"{nameof(DailySummaryCroneJob)} executed @ {DateTime.Now}");
                                }
                            }
                            catch (TimeoutException)
                            {
                                source.Cancel();
                                _logger.LogError($"{nameof(DailySummaryCroneJob)} operation has timed out");
                            }
                            finally
                            {
                                source.Dispose();
                            }

                        }
                        else
                        {
                            _logger.LogError($"{nameof(DailySummaryCroneJob)} next occurence time of process empty, cron failed. Please check configuration and/or restart process");
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {

                    if (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogError($"{nameof(DailySummaryCroneJob)} operation has been cancelled");
                    }
                    throw;
                }
            }
            else
            {
                _logger.LogError($"{nameof(DailySummaryCroneJob)} cron string not found in appsettings, please check configuration file and restart the process");
                await StopAsync(stoppingToken);
            }
        }

        private async Task<bool> ProcessDailySummaryAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            try
            {
                string tenantId = _cannedReportsettings.TenantId;
                string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath);
                string reportName = _cannedReportsettings.ReportName;
                string spName = _cannedReportsettings.StoredProcedureName;
                FlexAppContextBridge hostContextInfo = new FlexAppContextBridge()
                {
                    TenantId = _cronSettings.CronTenantId
                };
                AddFeedbackDto dto = new();
                dto.SetAppContext(hostContextInfo);
                _repoFactory.Init(dto);

                if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(reportName) || string.IsNullOrEmpty(spName))
                {
                    _logger.LogWarning("Missing required configuration: 'FilePath', 'ReportName', 'SPName'.");
                    return false;
                }
                
                _logger.LogInformation("Tenant Execution Start - {TenantId}", tenantId);

                DataTable dt = GetDataFromDB(spName, tenantId, out string message);

                if (dt != null) 
                {
                    List<DailySummary> summaries = MapDataTableToDailySummary(dt);
                    _logger.LogInformation($"Number of daily summaries: {summaries.Count}");

                    List<string> userCodes = summaries.Select(x => x.EncollectCode).Distinct().ToList();

                    List<string> userIds = await _repoFactory.GetRepo().FindAll<ApplicationUser>().ByCustomIds(userCodes)
                                                .Select(x => x.Id).ToListAsync();

                    var geoTagDetails = await _repoFactory.GetRepo().FindAll<GeoTagDetails>()
                                                                .ByToday()
                                                                .ByGeoTagUsers(userIds)
                                                                .ByReceiptOrTrailTransactionType()
                                                                .FlexInclude(x => x.ApplicationUser)
                                                                .Select(x => new { 
                                                                    x.Latitude,
                                                                    x.Longitude,
                                                                    x.CreatedDate,
                                                                    x.ApplicationUser.CustomId, 
                                                                    x.ApplicationUser.Id,
                                                                })
                                                                .ToListAsync();
                    _logger.LogInformation($"Number of geoTagDetails: {geoTagDetails.Count}");
                    foreach (var userCode in userCodes)
                    {
                        List<(double lat, double lng)> coordinates = geoTagDetails.Where(x => x.CustomId == userCode)
                                                                        .OrderBy(x => x.CreatedDate)
                                                                        .Select(x => (x.Latitude, x.Longitude))
                                                                        .ToList();
                        
                        double totalKm = await _distanceCalculatorService.CalculateTotalDistanceInKmAsync(coordinates);

                        summaries.FirstOrDefault(u => u.EncollectCode == userCode)!.DistanceTravelled = totalKm;
                        summaries.FirstOrDefault(u => u.EncollectCode == userCode)!.NoOfStops = coordinates.Count;
                        _logger.LogInformation($"UserCode: {userCode} | geoTagDetails: {coordinates.Count} | DistanceTravelled: {totalKm} | NoOfStops: {coordinates.Count}");
                    }

                    bool result = await GenerateAndEmailDailySummaryAsync(summaries, filePath, reportName, tenantId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProcessDailySummaryAsync)} exception occurred while processing daily summary");
                return false;
            }
            return true;
        }

        private DataTable GetDataFromDB(string spName,string tenantId, out string message)
        {
            var dt = new DataTable();
            message = "Error while fetching data";
            try
            {
                // Retrieve tenant info
                var tenant = _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId)
                   .Select(x => new
                   {
                       x.HostName,
                       x.DefaultWriteDbConnectionString
                   }).FirstOrDefault();

                var utility = _flexHost.GetUtilityService<DbUtilityFactory>();
                var dbTypeEnum = DBTypeEnum.FromValue<DBTypeEnum>(_databaseSettings.DBType.ToLower());
                var dbUtility = utility.GetUtility(dbTypeEnum);

                var request = new GetDataRequestDto
                {
                    SpName = spName,
                    TenantId = tenantId
                };

                dt = dbUtility.GetData(request).GetAwaiter().GetResult();
                if (dt.Rows.Count == 0)
                {
                    _logger.LogWarning("No data retrieved for stored procedure '{SpName}' and tenant '{TenantId}'", spName, tenantId);
                    message = "No data found.";
                }
                else
                {
                    message = "Data retrieval successful.";
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred while fetching data.";
                _logger.LogError(ex, "Exception occurred in GetDataFromDB for tenant '{TenantId}' and SP '{SpName}'", tenantId, spName);
            }
            return dt;
        }

        private List<DailySummary> MapDataTableToDailySummary(DataTable table)
        {
            var result = new List<DailySummary>();
            var properties = typeof(DailySummary).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (DataRow row in table.Rows)
            {
                var item = new DailySummary();

                foreach (var prop in properties)
                {
                    // Try to find a column with the same name (ignoring case and spaces)
                    var column = table.Columns
                        .Cast<DataColumn>()
                        .FirstOrDefault(c => string.Equals(c.ColumnName.Replace(" ", ""), prop.Name, StringComparison.OrdinalIgnoreCase));

                    if (column != null && row[column] != DBNull.Value)
                    {
                        try
                        {
                            object value = Convert.ChangeType(row[column], Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                            prop.SetValue(item, value);
                        }
                        catch (Exception ex)
                        {
                            // Optional: log or handle conversion issues
                            Console.WriteLine($"Error mapping property '{prop.Name}': {ex.Message}");
                        }
                    }
                }

                result.Add(item);
            }

            return result;
        }

        public bool ExportDailySummaryToFile(List<DailySummary> summaries, string filePath, string fileName)
        {
            bool isSuccess = false;

            try
            {
                // Get file extension from config
                string fileType = _fileConfigurationSettings.DefaultExtension;

                // Ensure directory exists
                Directory.CreateDirectory(filePath);

                string csvFile = $"{fileName}.{fileType}";
                string fullPath = Path.Combine(filePath, csvFile);

                // Delete existing file
                File.Delete(fullPath);

                // Export to CSV
                File.WriteAllText(fullPath, _csvExcelUtility.ConvertListToCsv(summaries));

                // Validate file and zip
                if (_fileValidationUtility.CheckIfFileExists(filePath, csvFile))
                {
                    string zipFile = $"{fileName}.zip";
                    string zipFilePath = Path.Combine(filePath, zipFile);

                    File.Delete(zipFilePath);

                    using (var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                    {
                        zip.CreateEntryFromFile(fullPath, csvFile);
                    }

                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while exporting DailySummary to file");
            }

            return isSuccess;
        }

        public async Task<bool> GenerateAndEmailDailySummaryAsync(List<DailySummary> summaries, string filePath, string reportName,string tenantId)
        {
            string fileName = $"{tenantId}_{Path.GetFileNameWithoutExtension(reportName)}_{DateTime.Now:yyyyMMdd}";
            string emailIds = _cannedReportsettings.EmailIds;
            var emails = emailIds.Split(',').Select(e => e.Trim()).ToList();

            string message = string.Empty;
            bool isGenerated = ExportDailySummaryToFile(summaries, filePath, fileName);

            string subject = $"{reportName} Canned Report - {DateTime.Now:dd-MM-yyyy}";
            string body;

            if (isGenerated)
            {
                string file = $"{fileName}.zip";
                string fullPath = Path.Combine(filePath, file);

                if (!File.Exists(fullPath))
                {
                    message = "ZIP file not found after generation.";
                    body = $"<p>Dear Team,<br/><br/>Report not attached due to: {message}.<br/>Contact Admin.</p>";
                }
                else
                {
                    int maxFileSize = _settings.MaxDownloadFileSizeMb;
                    long fileSize = new FileInfo(fullPath).Length;
                    long maxSizeInBytes = maxFileSize * 1024 * 1024;

                    if (fileSize > maxSizeInBytes)
                    {
                        message = $"File size exceeds {maxFileSize}MB";
                        body = $"<p>Dear Team,<br/><br/>Report not attached due to: {message}.<br/>Contact Admin.</p>";
                    }
                    else
                    {
                        body = "<p>Dear Team,<br/><br/>Please find the attached report.</p>";
                        var files = new List<string> { file };

                        foreach (var email in emails)
                        {
                            await _emailUtility.SendEmailAsync(email, body, subject, tenantId, files, filePath);
                        }

                        return true; // Success
                    }
                }
            }
            else
            {
                message = string.IsNullOrEmpty(message)? "Unknown error while generating report." : message;
                body = $"<p>Dear Team,<br/><br/>Report not generated due to: {message}.<br/>Contact Admin.</p>";
            }

            // Send error notification to all recipients
            foreach (var email in emails)
            {
                await _emailUtility.SendEmailAsync(email, body, subject, tenantId);
            }

            return false;
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

        public class DailySummary
        {
            public string CollectorBranchOrAgency { get; set; }
            public string AllocatedAgentOrStaffName { get; set; }
            public string EncollectCode { get; set; }
            public int AccountsAllocated { get; set; }
            public string TransactionDate { get; set; }
            public int AccountsWithTrails { get; set; }
            public int AccountsWithPayments { get; set; }
            public int AccountsWithPTP { get; set; }
            public int NoOfVisits { get; set; }            
            public int NoOfStops { get; set; }
            public double DistanceTravelled { get; set; }
        }
        #endregion
    }
}
