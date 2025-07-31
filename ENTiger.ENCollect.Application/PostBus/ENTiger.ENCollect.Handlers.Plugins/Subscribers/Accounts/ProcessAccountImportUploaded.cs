using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;
using System.Net;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// Handles the process of importing uploaded account files using either MySQL or SSIS-based flows.
    /// </summary>
    public partial class ProcessAccountImportUploaded : IProcessAccountImportUploaded
    {
        // Dependencies
        protected readonly ILogger<ProcessAccountImportUploaded> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        protected readonly PackageSSISProviderFactory _packageSSISProviderFactory;
        protected readonly ICsvExcelUtility _csvExcelUtility;
        protected readonly FilePathSettings _fileSettings;
        protected readonly DatabaseSettings _databaseSettings;
        protected readonly IFileSystem _fileSystem;
        protected readonly FileConfigurationSettings _fileConfigurationSettings;

        // Runtime variables
        private FlexAppContextBridge? _flexAppContext;
        private MasterFileStatus? _model;
        private RunStatus _runStatusModel;
        private string _dbType = string.Empty;
        private string tenantId = string.Empty;
        private string hostName = string.Empty;
        private char _delimiter;
        string message = string.Empty;
        string errorFileName = string.Empty;
        private string EventCondition = string.Empty;

        // Constants and reusable DTOs
        private const string spName = "usp_ImportAccounts_";
        private readonly ExecuteSpRequestDto request = new();
        private readonly GetDataRequestDto requestGetData = new();
        private readonly InsertIntermediateTableRequestDto insertRequest = new();

        private IDbUtility _dbUtility;

        public ProcessAccountImportUploaded(
            ILogger<ProcessAccountImportUploaded> logger,
            IRepoFactory repoFactory,
            IFlexHost flexHost,
            PackageSSISProviderFactory packageSSISProviderFactory,
            ICsvExcelUtility csvExcelUtility,
            IOptions<FilePathSettings> fileSettings,
            IOptions<DatabaseSettings> databaseSettings,
            IFileSystem fileSystem,
            IOptions<FileConfigurationSettings> fileConfigurationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _flexHost = flexHost;
            _packageSSISProviderFactory = packageSSISProviderFactory;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
        }

        public virtual async Task Execute(AccountImportUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("Start Account Import | JSON: {Event}", JsonConvert.SerializeObject(@event));

            InitializeContext(@event);
            ConfigureDBUtilityInstance();
            _model = await LoadMasterFileStatusAsync(@event.Id);

            var filePath = Path.Combine(_model.FilePath, _model.FileName);
            var dt = await ConvertToDataTableAsync(filePath);

            if (!await ValidateHeadersAsync(dt))
            {
                await HandleInvalidHeadersAsync();
                await this.Fire<ProcessAccountImportUploaded>(EventCondition, serviceBusContext);
                return;
            }

            if (IsMySql)
                await HandleMySqlImportFlow(dt);
            else
                await HandleSSISFlow(filePath, @event.Id);

            _logger.LogInformation("Completed Account Import | EventCondition: {Condition}", EventCondition);
            await this.Fire<ProcessAccountImportUploaded>(EventCondition, serviceBusContext);
        }

        private void InitializeContext(AccountImportUploadedEvent @event)
        {
            _dbType = _databaseSettings.DBType;
            _delimiter = Convert.ToChar(_fileConfigurationSettings.Delimiter);
            _flexAppContext = @event.AppContext!;
            _repoFactory.Init(@event);
            tenantId = _flexAppContext.TenantId;
            hostName = _flexAppContext.HostName;
        }

        private bool IsMySql => string.Equals(_dbType, "mysql", StringComparison.OrdinalIgnoreCase);

        private void ConfigureDBUtilityInstance()
        {
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(_flexAppContext.HostName);
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(_dbType.ToLower());
            _dbUtility = utility.GetUtility(dbUtilityEnum);
        }

        private async Task<MasterFileStatus> LoadMasterFileStatusAsync(string id)
        {
            return await _repoFactory.GetRepo().FindAll<MasterFileStatus>()
                .FirstAsync(x => x.Id == id);
        }

        private async Task<DataTable> ConvertToDataTableAsync(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase)
                ? _csvExcelUtility.CsvToDataTable(filePath, _delimiter, _model.CustomId)
                : _csvExcelUtility.ExcelToDataTable(filePath, true, _model.CustomId);
        }

        private async Task HandleInvalidHeadersAsync()
        {
            await UpdateFileStatus(_model, FileStatusEnum.InvalidFileFormat.Value);
            message = "InvalidHeaders";
            EventCondition = CONDITION_ONFAILURE;
        }

        private async Task HandleMySqlImportFlow(DataTable dt)
        {
            var csvFile = CreateCsvFromDataTable(dt);
            await UpdateFile(_model, "ToCSV");

            var imported = await InsertIntermediateAsync(csvFile);
            await UpdateFile(_model, $"ImportedToIntermediateTable - {imported}");

            if (imported == 0) return;

            foreach (var phase in new[] { "Validate", "SetFlag", "Update", "Insert" })
                await ExecuteSpAndUpdate(phase);

            await ProcessImportedRecordsAsync();
        }

        private string CreateCsvFromDataTable(DataTable dt)
        {
            var path = Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath, $"{_model.CustomId}.csv");
            _csvExcelUtility.ToCSV(dt, path);
            return path;
        }

        private async Task<int> InsertIntermediateAsync(string csvFile)
        {
            insertRequest.Delimiter = _delimiter.ToString();
            insertRequest.TableName = "ImportAccounts_Intermediate";
            insertRequest.FileName = csvFile;
            insertRequest.TenantId = tenantId;
            return await _dbUtility.InsertRecordsIntoIntermediateTable(insertRequest);
        }

        private async Task ExecuteSpAndUpdate(string suffix)
        {
            request.SpName = spName + suffix;
            request.TenantId = tenantId;
            request.Parameters = new Dictionary<string, string> { { "WorkRequestId", _model.CustomId } };
            var result = await _dbUtility.ExecuteSP(request);
            await UpdateFile(_model, $"{suffix} - {result}");
        }

        private async Task ProcessImportedRecordsAsync()
        {
            requestGetData.WorkRequestId = _model.CustomId;
            requestGetData.SpName = spName + "GetData";
            requestGetData.TenantId = tenantId;
            requestGetData.Parameters = new Dictionary<string, string> { { "WorkRequestId", _model.CustomId } };

            var records = await _dbUtility.GetData(requestGetData);
            if (records == null) return;

            var errorRecords = records.Select("IsError = true").ToList();
            int totalRecords = records.Rows.Count;
            int errorCount = errorRecords.Count;
            int successCount = totalRecords - errorCount;
            if (errorCount == 0)
            {
                await UpdateFileStatus(_model, "Processed");
                await UpdateFile(_model, "SentSuccessMail");
                EventCondition = CONDITION_ONSUCCESS;
            }
            else if (successCount > 0)
            {
                await UpdateFileStatus(_model, "Partially Processed");
                await UpdateFile(_model, "SentPartialErrorMail");
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                await UpdateFileStatus(_model, "Failed");
                errorFileName = Path.Combine(_model.FilePath, $"{_model.CustomId}_Errors.csv");
                _csvExcelUtility.ToCSV(errorRecords.CopyToDataTable(), errorFileName);
                await UpdateFile(_model, "SentFailedErrorMail");
                EventCondition = CONDITION_ONFAILURE;
            }

            await ExecuteSpAndUpdate("Archive");
        }

        private async Task HandleSSISFlow(string filePath, string id)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(_model.FileName);

            if (ext == ".xlsx")
            {
                string csvPath = Path.Combine(_model.FilePath, $"{fileNameWithoutExt}.csv");
                var dt = await ConvertToDataTableAsync(filePath);
                _csvExcelUtility.ToCSV(dt, csvPath);
            }

            string result = await ProcessSSISPackageAsync(_model.CustomId, $"{fileNameWithoutExt}.csv", id);
            EventCondition = string.IsNullOrEmpty(result) || result.Equals("failure", StringComparison.OrdinalIgnoreCase)
                ? CONDITION_ONFAILURE
                : CONDITION_ONSUCCESS;
        }

        private async Task<string> ProcessSSISPackageAsync(string customId, string fileName, string id)
        {
            var dto = new BulkUploadFileDto
            {
                CustomId = customId,
                FileName = fileName,
                FileType = "AccountImportFile"
            };
            dto.SetAppContext(_flexAppContext);
            _runStatusModel = _flexHost.GetDomainModel<RunStatus>().StartRun(dto.FileType, dto.CustomId, dto.GetAppContext().UserId);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            await _repoFactory.GetRepo().SaveAsync();

            var ssisPackage = _packageSSISProviderFactory.GetSSISPackageProvider(dto.FileType);
            var result = (await ssisPackage.ExecutePackage(dto, id))?.PackageExecResult ?? "";

            _runStatusModel.FinishRun();
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            await _repoFactory.GetRepo().SaveAsync();

            return result;
        }

        private async Task<bool> ValidateHeadersAsync(DataTable dataTable)
        {
            var staticHeaders = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                .Where(x => x.CategoryMasterId == CategoryMasterEnum.ImportAccountsHeaders.Value)
                .OrderBy(x => x.Id)
                .Select(x => x.Name.Trim())
                .ToListAsync();

            var dynamicHeaders = dataTable.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName?.Trim())
                .ToArray();

            return Enumerable.SequenceEqual(staticHeaders, dynamicHeaders);
        }

        private async Task UpdateFile(MasterFileStatus model, string status)
        {
            model.Description += $" -> {status}";
            model.SetAddedOrModified();
            _repoFactory.GetRepo().InsertOrUpdate(model);
            await _repoFactory.GetRepo().SaveAsync();
        }

        private async Task UpdateFileStatus(MasterFileStatus model, string status)
        {
            model.Status = status;
            model.FileProcessedDateTime = DateTime.Now;
            _repoFactory.GetRepo().InsertOrUpdate(model);
            await _repoFactory.GetRepo().SaveAsync();
        }
    }
}
