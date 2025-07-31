using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;
using System.IO.Compression;

namespace ENTiger.ENCollect
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFileValidationUtility _fileValidationUtility;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly IEmailUtility _emailUtility;
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly IFlexHost _flexHost;
        private readonly NotificationSettings _notificationSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly FileValidationSettings _settings;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _fileSettings;
        private readonly CannedReportSetting _cannedReportsettings;
        public Worker(
            ILogger<Worker> logger,
            IEmailUtility emailUtility,
            IFlexHost flexHost,
            IFileValidationUtility fileValidationUtility,
            ICsvExcelUtility csvExcelUtility,
            IOptions<DatabaseSettings> databaseSettings,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IOptions<FileValidationSettings> settings,
            IServiceProvider serviceProvider,
           IFileSystem fileSystem,
           IOptions<FilePathSettings> fileSettings,
           IOptions<CannedReportSetting> cannedReportsettings
           )  // Injected ServiceProvider
        {
            _logger = logger;
            _fileValidationUtility = fileValidationUtility;
            _csvExcelUtility = csvExcelUtility;
            _emailUtility = emailUtility;
            _repoTenantFactory = serviceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _flexHost = flexHost;
            _databaseSettings = databaseSettings.Value;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _settings = settings.Value;
            _fileSystem = fileSystem;
            _fileSettings = fileSettings.Value;
            _cannedReportsettings = cannedReportsettings.Value;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Canned Report: Start");

            int maxFileSize = _settings.MaxDownloadFileSizeMb;
            string emailIds = _cannedReportsettings.EmailIds;
            string tenantId = _cannedReportsettings.TenantId;
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(emailIds))
            {
                _logger.LogWarning("Please configure 'TenantIds' & 'ToEmailIds' in appsettings.json.");
                return;
            }
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath);
            string reportName = _cannedReportsettings.ReportName;
            string spName = _cannedReportsettings.StoredProcedureName;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(reportName) || string.IsNullOrEmpty(spName))
            {
                _logger.LogWarning("Missing required configuration: 'FilePath', 'ReportName', 'SPName'.");
                return;
            }
            if (spName.Contains("AllocationRemove"))
            {
                bool isGenerated = await AllocationRemove(spName, tenantId);
            }
            else
            {

                try
                {
                    string message = string.Empty;
                    string fileName = $"{tenantId}_{Path.GetFileNameWithoutExtension(reportName)}_{DateTime.Now:yyyyMMdd}";
                    _logger.LogInformation("Tenant Execution Start - {TenantId}", tenantId);

                    bool isGenerated = await GetDataFromDBAndGenerateFile(filePath, fileName, spName, tenantId);
                    var emails = emailIds.Split(',').Select(e => e.Trim()).ToList();
                    _logger.LogInformation("Email Recipients Count: {Count}", emails.Count);

                    string subject = $"{reportName} Canned Report - {DateTime.Now:dd-MM-yyyy}";
                    string body;

                    if (isGenerated)
                    {
                        string file = $"{fileName}.zip";
                        string fullPath = Path.Combine(filePath, file);
                        long fileSize = new FileInfo(fullPath).Length;
                        long maxSizeInBytes = maxFileSize * 1024 * 1024;

                        if (fileSize > maxSizeInBytes)
                        {
                            message = $"File size exceeds {maxFileSize}MB";
                            body = $"<p>Dear Team,<br/><br/>Report not attached due to: {message}.<br/>Contact Admin.</p>";

                            foreach (var email in emails)
                            {
                                await _emailUtility.SendEmailAsync(email, body, subject, tenantId);
                            }
                        }
                        else
                        {
                            body = "<p>Dear Team,<br/><br/>Please find the attached report.</p>";
                            var files = new List<string> { file };

                            foreach (var email in emails)
                            {
                                await _emailUtility.SendEmailAsync(email, body, subject, tenantId, files, filePath);
                            }
                        }
                    }
                    else
                    {
                        body = $"<p>Dear Team,<br/><br/>Report not generated due to: {message}.<br/>Contact Admin.</p>";

                        foreach (var email in emails)
                        {
                            await _emailUtility.SendEmailAsync(email, body, subject, tenantId);
                        }
                    }
                    _logger.LogInformation("Tenant Execution End");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception in ExecuteAsync");
                }
            }
            System.Environment.Exit(0);
        }

        private async Task<bool> GetDataFromDBAndGenerateFile(string filePath, string fileName, string spName, string tenantId)
        {
            _logger.LogInformation("GetDataFromDBAndGenerateFile: Start");
            bool isSuccess = false;

            try
            {
                
                var utility = _flexHost.GetUtilityService<DbUtilityFactory>();
                var dbTypeEnum = DBTypeEnum.FromValue<DBTypeEnum>(_databaseSettings.DBType.ToLower());
                var dbUtility = utility.GetUtility(dbTypeEnum);

                var request = new GetDataRequestDto
                {
                    SpName = spName,
                   TenantId = tenantId
                };

                DataTable dt = await dbUtility.GetData(request);
                if (dt.Rows.Count == 0)
                {
                    _logger.LogWarning("No data retrieved for {SpName}", spName);
                    return false;
                }

                string fileType = _fileConfigurationSettings.DefaultExtension;
                Directory.CreateDirectory(filePath); // Ensure directory exists

                string csvFile = $"{fileName}.{fileType}";
                string fullPath = Path.Combine(filePath, csvFile);
                File.Delete(fullPath);

                _csvExcelUtility.ToCSV(dt, fullPath);

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
                _logger.LogError(ex, "Exception in GetDataFromDBAndGenerateFile");
            }

            _logger.LogInformation("GetDataFromDBAndGenerateFile: End");
            return isSuccess;
        }
        private async Task<bool> AllocationRemove(string spName, string tenantId)
        {
            _logger.LogInformation("GetDataFromDBAndGenerateFile: Start");
            bool isSuccess = false;

            try
            {

                var utility = _flexHost.GetUtilityService<DbUtilityFactory>();
                var dbTypeEnum = DBTypeEnum.FromValue<DBTypeEnum>(_databaseSettings.DBType.ToLower());
                var dbUtility = utility.GetUtility(dbTypeEnum);

                var request = new ExecuteSpRequestDto
                {
                    SpName = spName,
                    TenantId = tenantId
                };

                bool status = await dbUtility.ExecuteSP(request);
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetDataFromDBAndGenerateFile");
            }

            _logger.LogInformation("GetDataFromDBAndGenerateFile: End");
            return isSuccess;
        }
    }

}