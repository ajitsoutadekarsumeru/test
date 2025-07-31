using Elastic.Clients.Elasticsearch.Snapshot;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// Handles the processing of primary allocation files, including validation, bulk inserts, 
    /// stored procedure execution, and reporting.
    /// </summary>
    public partial class ProcessPrimaryAllocationFile : IProcessPrimaryAllocationFile
    {
        protected readonly ILogger<ProcessPrimaryAllocationFile> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        protected readonly PackageSSISProviderFactory _packageSSISProviderFactory;
        protected readonly DatabaseSettings _databaseSettings;
        protected readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileTransferUtility _fileTransferUtility;
        private readonly IFileSystem _fileSystem;
        private IDbUtility _dbUtility;

        private FlexAppContextBridge? _flexAppContext;
        private RunStatus _runStatusModel;
        private PrimaryAllocationFile? _model;
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();

        private string _dbType = string.Empty;
        private string _tempFolderPath;
        private string _rootFolderPath;
        private string _destinationPath;
        private char _delimiter;
        private string _tenantId;
        private string? _fileId;
        private string? _allocationType;
        private string? _allocationByType;
        private string? _allocationMethod;
        private string EventCondition = "";
        private int? insertCount = 0;

        private string _successProcessedPath;
        private string _failedProcessedPath;
        private string _partialProcessedPath;
        private string _invalidProcessedPath;
        private string _errorProcessedPath;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessPrimaryAllocationFile"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging execution details.</param>
        /// <param name="repoFactory">Factory for creating repository instances.</param>
        /// <param name="flexHost">Flex service host instance.</param>
        /// <param name="packageSSISProviderFactory">Factory for SSIS package providers.</param>
        /// <param name="primaryAllocation">Service for handling MySQL primary allocation.</param>
        /// <param name="databaseSettings">Database settings configuration.</param>
        /// <param name="csvExcelUtility">Utility for handling CSV and Excel operations.</param>
        /// <param name="fileSettings">Settings for file paths.</param>
        /// <param name="fileSystem">Abstraction for file system operations.</param>
        /// <param name="fileConfigurationSettings">Configuration settings for file processing.</param>
        public ProcessPrimaryAllocationFile(
            ILogger<ProcessPrimaryAllocationFile> logger,
            IRepoFactory repoFactory,
            IFlexHost flexHost,
            PackageSSISProviderFactory packageSSISProviderFactory,
            IOptions<DatabaseSettings> databaseSettings,
            ICsvExcelUtility csvExcelUtility,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IFileTransferUtility fileTransferUtility
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _flexHost = flexHost;
            _packageSSISProviderFactory = packageSSISProviderFactory;
            _databaseSettings = databaseSettings.Value;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }
        /// <summary>
        /// Executes the primary allocation file processing workflow.
        /// </summary>
        /// <param name="eventData">Event data containing details about the uploaded allocation file.</param>
        /// <param name="serviceBusContext">Context for interacting with the Flex service bus.</param>
        public virtual async Task Execute(PrimaryAllocationUploadedEvent eventData, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = eventData.AppContext;
            _repoFactory.Init(eventData);
            _allocationMethod = eventData.AllocationMethod.Equals("CustomerId Level", StringComparison.OrdinalIgnoreCase) ? "CustomerIdLevel" : "AccountLevel";

            SetVariables(eventData);
            ConfigureDBUtilityInstance();

            _model = await _repoFactory.GetRepo().FindAll<PrimaryAllocationFile>().FlexNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == _fileId);

            string filePath = _fileSystem.Path.Combine(_rootFolderPath, eventData.FileName);

            //  File Existence Check
            if (!_fileSystem.File.Exists(filePath))
            {
                _logger.LogError($"ProcessPrimaryAllocationFile: File not found: {eventData.FileName}");
                await UpdateFileStatus(FileStatusEnum.InvalidFileFormat.Value);
                EventCondition = CONDITION_ONFAILURE;
                return;
            }
            // 1. Get headers once and store
            var headers = _csvExcelUtility.GetExcelHeaders(filePath);

            // 2. Use headers for validation
            bool headersValid = await ValidateHeadersAsync(headers, _allocationType!, _allocationMethod);


            List<string> dynamicHeaders = headers.Columns.Cast<DataColumn>().Select(x => x.ColumnName?.Trim()).ToList();


            if (!headersValid)
            {
                await UpdateFileStatus(FileStatusEnum.InvalidFileFormat.Value);
                EventCondition = CONDITION_ONFAILURE;
            }
            else
            {
                if (string.Equals(_dbType, DBTypeEnum.MySQL.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    bool isSuccess = await ProcessMySqlPrimaryAllocationAsync(eventData, dynamicHeaders);
                    EventCondition = isSuccess ? CONDITION_ONSUCCESS : CONDITION_ONFAILURE;
                }
                else if (string.Equals(_dbType, DBTypeEnum.MsSQL.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    bool isSuccess = await ProcessSSISPackage(eventData);
                    EventCondition = isSuccess ? CONDITION_ONSUCCESS : CONDITION_ONFAILURE;
                }
            }

            await this.Fire<ProcessPrimaryAllocationFile>(EventCondition, serviceBusContext);
        }
        /// <summary>
        /// Sets necessary variables required for allocation file processing.
        /// </summary>
        /// <param name="eventData">Uploaded file event data.</param>
        private void SetVariables(PrimaryAllocationUploadedEvent eventData)
        {
            _logger.LogInformation($"ProcessPrimaryAllocationFile: Starting allocation file processing | DBType - {_databaseSettings.DBType}");
            _fileId = eventData.Id;
            _allocationType = eventData.FileType;
            _allocationByType = _allocationMethod;
            _tenantId = _flexAppContext.TenantId;
            _delimiter = Convert.ToChar(_fileConfigurationSettings.Delimiter);
            _dbType = _databaseSettings.DBType ?? string.Empty;


            _rootFolderPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _tempFolderPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.TemporaryPath);
            _destinationPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.AllocationProcessedFilePath);
            _successProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.SuccessProcessedFilePath);
            _failedProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.FailedProcessedFilePath);
            _partialProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.PartialProcessedFilePath);
            _invalidProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.InvalidProcessedFilePath);
            _errorProcessedPath = _fileSystem.Path.Combine(_destinationPath); 
        }
        /// <summary>
        /// Configures the database utility instance based on the database type.
        /// </summary>
        private void ConfigureDBUtilityInstance()
        {
            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(_flexAppContext.HostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(_dbType.ToLower());
            _dbUtility = utility.GetUtility(dbUtilityEnum);
        }
        /// <summary>
        /// Processes MySQL primary allocation asynchronously.
        /// </summary>
        private async Task<bool> ProcessMySqlPrimaryAllocationAsync(PrimaryAllocationUploadedEvent eventData, List<string> headers)
        {
            _logger.LogInformation("ProcessPrimaryAllocationFile: Processing MySQL Allocation | FileId: {FileId}, FileType: {FileType}, AllocationMethod: {AllocationMethod}",
                _fileId, _allocationType, _allocationByType);

            try
            {
                string csvFilePath = ConvertExcelToCsv(headers);
                if (string.IsNullOrWhiteSpace(csvFilePath))
                {
                    _logger.LogError("ProcessPrimaryAllocationFile: CSV file conversion failed | FileId: {FileId}", _fileId);
                    await UpdateFileStatus(FileStatusEnum.Failed.Value);
                    return false;
                }

                int insertedCount = await BulkInsertIntoIntermediateTable(csvFilePath);
                if (insertedCount <= 0)
                {
                    _logger.LogError("ProcessPrimaryAllocationFile: Bulk insert into intermediate table failed | FileId: {FileId}", _fileId);
                    await UpdateFileStatus(FileStatusEnum.Failed.Value);
                    return false;
                }

                insertCount = insertedCount;
                await ExecuteStoredProceduresAsync();

                _logger.LogInformation("ProcessPrimaryAllocationFile: MySQL Primary Allocation Processing Completed Successfully | FileId: {FileId}", _fileId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ProcessPrimaryAllocationFile: An error occurred while processing MySQL primary allocation | FileId: {FileId}", _fileId);
                await UpdateFileStatus(FileStatusEnum.Error.Value);
                return false;
            }
        }

        /// <summary>
        /// Processes the SSIS package for primary allocation.
        /// </summary>
        /// <param name="eventData">Event data containing file details for processing.</param>
        /// <returns>Returns the result of the SSIS package execution.</returns>
        private async Task<bool> ProcessSSISPackage(PrimaryAllocationUploadedEvent eventData)
        {
            _logger.LogInformation("ProcessPrimaryAllocationFile: Processing SSIS Package | FileId: {FileId}", _fileId);

            var dto = CreateBulkUploadDto(eventData);
            LogInputJson(dto);

            await InitializeRunStatusAsync(dto);

            var result = await ExecuteSSISPackage(dto);

            await CompleteRunStatusAsync(); // Changed to async

            // Determine success or failure based on result
            if (string.IsNullOrEmpty(result) || string.Equals(result, "failure", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError("ProcessPrimaryAllocationFile: SSIS Package execution failed | FileId: {FileId}", _fileId);
                await UpdateFileStatus(FileStatusEnum.Failed.Value);
                return false;
            }
            // Move the processed file from the root folder path to the destination path, allowing overwrite if the destination file already exists

            await _fileTransferUtility.RenameAndMoveFileAsync(_rootFolderPath, _successProcessedPath, _model.FileName);
            _logger.LogInformation("ProcessPrimaryAllocationFile: SSIS Package executed successfully | FileId: {FileId}", _fileId);
            return true;
        }

        /// <summary>
        /// Creates a DTO object for bulk upload based on the event data.
        /// </summary>
        /// <param name="eventData">The event data containing file details.</param>
        /// <returns>A populated <see cref="BulkUploadFileDto"/> object.</returns>
        private BulkUploadFileDto CreateBulkUploadDto(PrimaryAllocationUploadedEvent eventData)
        {
            var dto = new BulkUploadFileDto
            {
                CustomId = eventData.CustomId,
                FileType = eventData.FileType,
                FileName = eventData.FileName,
                AllocationMethod = _allocationByType
            };

            dto.SetAppContext(_flexAppContext);
            return dto;
        }


        /// <summary>
        /// Marks the completion of the run status and saves the status asynchronously.
        /// </summary>
        private async Task CompleteRunStatusAsync()
        {
            _logger.LogInformation($"ProcessPrimaryAllocationFile: Run status completed: {_runStatusModel.CustomId}");
            _runStatusModel.FinishRun();
            await SaveRunStatus();
        }
        /// <summary>
        /// Logs the input JSON for debugging and tracking purposes.
        /// </summary>
        /// <param name="dto">The DTO object containing bulk upload data.</param>
        private void LogInputJson(BulkUploadFileDto dto)
        {
            string inputJson = JsonConvert.SerializeObject(dto);
            _logger.LogInformation("ProcessPrimaryAllocationFile: Input JSON: {Json}", inputJson);
        }
        /// <summary>
        /// Initializes the run status for tracking the file processing status.
        /// </summary>
        /// <param name="dto">The bulk upload file DTO containing file details.</param>
        private async Task InitializeRunStatusAsync(BulkUploadFileDto dto)
        {
            _runStatusModel = _flexHost
                .GetDomainModel<RunStatus>()
                .StartRun(dto.FileType, dto.CustomId, dto.GetAppContext().UserId);

            await SaveRunStatus();
        }
        /// <summary>
        /// Saves the current run status into the repository.
        /// </summary>
        private async Task SaveRunStatus()
        {
            var repo = _repoFactory.GetRepo();
            repo.InsertOrUpdate(_runStatusModel);
            await repo.SaveAsync();
        }
        /// <summary>
        /// Validates the headers of the uploaded data table.
        /// </summary>
        /// <param name="dataTable">The data table containing uploaded records.</param>
        /// <param name="type">The type of allocation.</param>
        /// <param name="allocationByType">The method of allocation.</param>
        /// <returns>Returns <c>true</c> if the headers are valid, otherwise <c>false</c>.</returns>
        private async Task<bool> ValidateHeadersAsync(DataTable dataTable, string type, string allocationByType)
        {
            if (dataTable == null || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(allocationByType))
            {
                _logger.LogWarning("Invalid parameters for header validation.");
                return false;
            }

            var dynamicHeaders = dataTable.Columns
                .Cast<DataColumn>()
                .Select(x => x.ColumnName?.Trim())
                .ToArray();

            var staticHeaders = await _repoFactory.GetRepo()
                                    .FindAll<CategoryItem>()
                                    .Where(x => x.CategoryMasterId == CategoryMasterEnum.PrimaryAllocationHeaders.ToString()
                                             && x.Code.Trim() == type.Trim()
                                             && x.Description.Trim() == allocationByType.Trim())
                                    .OrderBy(x => x.Id)
                                    .Select(x => x.Name.Trim())
                                    .ToListAsync();

            return staticHeaders.SequenceEqual(dynamicHeaders);
        }
        /// <summary>
        /// Marks an allocation file as invalid due to incorrect format.
        /// </summary>
        /// <param name="allocationFile">The allocation file to be marked as invalid.</param>
        private async Task MarkAllocationFileAsInvalid(PrimaryAllocationFile allocationFile)
        {
            _logger.LogWarning($"ProcessPrimaryAllocationFile: Invalid file format detected | FileId: {allocationFile.Id}");
            allocationFile.Status = FileStatusEnum.InvalidFileFormat.Value;
            allocationFile.SetAddedOrModified();

            _repoFactory.GetRepo().InsertOrUpdate(allocationFile);
            await _repoFactory.GetRepo().SaveAsync();
        }
        /// <summary>
        /// Updates the file processing status in the database.
        /// </summary>
        /// <param name="status">The new status to be set for the file.</param>
        private async Task UpdateFileStatus(string status)
        {
            var destinationPath = GetProcessedPath(status);

            _model.Status = status;
            _model.FilePath = destinationPath;
            _model.SetAddedOrModified();

            // Update the file status in the database
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            await _repoFactory.GetRepo().SaveAsync();

            // Move the processed file from the root folder path to the destination path, allowing overwrite if the destination file already exists
            await _fileTransferUtility.RenameAndMoveFileAsync(_rootFolderPath, destinationPath, _model.FileName);
        }

        /// <summary>
        /// Executes the SSIS package for processing the uploaded file.
        /// </summary>
        /// <param name="dto">The DTO containing file processing details.</param>
        /// <returns>Returns the result of SSIS package execution.</returns>
        private async Task<string> ExecuteSSISPackage(BulkUploadFileDto dto)
        {
            var ssisPackageProvider = _packageSSISProviderFactory.GetSSISPackageProvider(UploadTypeEnum.PrimaryAllocation.ToString());
            var ssisResult = await ssisPackageProvider.ExecutePackage(dto, _model.Id);

            return ssisResult?.PackageExecResult ?? string.Empty;
        }
        /// <summary>
        /// Performs a bulk insert into an intermediate database table.
        /// </summary>
        /// <param name="filePath">The file path of the uploaded data.</param>
        /// <returns>Returns the number of records inserted.</returns>
        private async Task<int> BulkInsertIntoIntermediateTable(string filePath)
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Inserting into Intermediate Table");

            var request = new InsertIntermediateTableRequestDto
            {
                Delimiter = _delimiter.ToString(),
                TableName = PrimaryAllocationStoredProcedureEnum.InsertIntermediateTable.ToString(),
                FileName = filePath,
                TenantId = _tenantId
            };

            return await _dbUtility.InsertRecordsIntoIntermediateTable(request);
        }
        /// <summary>
        /// Converts an Excel file to CSV format.
        /// </summary>
        private string ConvertExcelToCsv(List<string> headers)
        {
            _logger.LogInformation("ProcessPrimaryAllocationFile: Converting Excel to CSV");
            string excelFilePath = _fileSystem.Path.Combine(_rootFolderPath, _model.FileName);

            //  File Existence Check
            if (!_fileSystem.File.Exists(excelFilePath))
            {
                _logger.LogError("ProcessPrimaryAllocationFile: File not found: {FileName}", _model.FileName);
                return string.Empty;
            }

            string csvFileName = Path.ChangeExtension(_model.FileName, ".csv");
            string csvFilePath = _fileSystem.Path.Combine(_tempFolderPath, csvFileName);
            ExcelFilterTypeEnum filterTypeEnum = _allocationByType == "CustomerIdLevel" ? ExcelFilterTypeEnum.CustomerUpload : ExcelFilterTypeEnum.AccountUpload;
            _csvExcelUtility.ConvertExcelToCsv(_rootFolderPath, _tempFolderPath, _model.CustomId, Path.GetFileNameWithoutExtension(_model.FileName), filterTypeEnum.Value, headers);

            return csvFilePath;
        }

        /// <summary>
        /// Executes a series of stored procedures for primary allocation processing.
        /// </summary>
        private async Task ExecuteStoredProceduresAsync()
        {
            _logger.LogInformation("ProcessPrimaryAllocationFile: Executing Stored Procedures");
            // Base parameters for all stored procedures
            var baseParameters = new Dictionary<string, string>
            {
                { "WorkRequestId", _model.CustomId },
                { "AllocationType", _allocationByType }
            };

            // Execute validation stored procedure
            var validationSP = GetValidationStoredProcedure(_allocationType);
            if (!string.IsNullOrEmpty(validationSP))
            {
                await ExecuteStoredProcedureAsync(validationSP, baseParameters);
            }

            // Execute insert into main and error table stored procedure
            var insertParameters = new Dictionary<string, string>(baseParameters)
            {
                { "ExcelRowCount", insertCount?.ToString() }
            };
            await ExecuteStoredProcedureAsync(PrimaryAllocationStoredProcedureEnum.InsertIntoMainAndErrorTable.ToString(), insertParameters);

            // Execute allocation stored procedure
            var allocationSP = GetAllocationStoredProcedure(_allocationType);
            if (!string.IsNullOrEmpty(allocationSP))
            {
                await ExecuteStoredProcedureAsync(allocationSP, baseParameters);
            }

            // Insert allocation history
            var allocationHistoryParameters = new Dictionary<string, string>
            {
                { "WorkRequestId", _model.CustomId },
                { "UserId", _model?.CreatedBy },
                { "TypeOfAllocation", _allocationByType },
                { "MethodofAllocation", "Manual File Upload" }
            };
            await ExecuteStoredProcedureAsync(PrimaryAllocationStoredProcedureEnum.InsertAllocationHistory.ToString(), allocationHistoryParameters);

            // Generate CSV report for processed allocation
            GenerateCsvReportAsync();

            // Cleanup records after processing
            var deleteParameters = new Dictionary<string, string> { { "WorkRequestId", _model.CustomId } };
            await ExecuteStoredProcedureAsync(PrimaryAllocationStoredProcedureEnum.CleanupRecords.ToString(), deleteParameters);

            // Move the processed file from the root folder path to the destination path, allowing overwrite if the destination file already exists
            _model = await _repoFactory.GetRepo().FindAll<PrimaryAllocationFile>()
                                            .FlexNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == _fileId);
            await UpdateFileStatus(_model.Status);
        }
        /// <summary>
        /// Executes a stored procedure asynchronously.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure.</param>
        /// <param name="parameters">The parameters to be passed to the stored procedure.</param>
        private async Task ExecuteStoredProcedureAsync(string storedProcedure, Dictionary<string, string> parameters)
        {
            request.SpName = storedProcedure;
            request.TenantId = _tenantId;
            request.Parameters = parameters;

            _logger.LogInformation($"ProcessPrimaryAllocationFile: Executing SP: {storedProcedure} for WorkRequestId: {_model.CustomId}");

            await _dbUtility.ExecuteSP(request);
            _logger.LogInformation($"ProcessPrimaryAllocationFile: Execution Completed: {storedProcedure}");
        }
        /// <summary>
        /// Gets the validation stored procedure name based on the allocation type.
        /// </summary>
        /// <param name="allocationType">The type of allocation.</param>
        /// <returns>The name of the validation stored procedure.</returns>
        private string GetValidationStoredProcedure(string allocationType)
        {
            return allocationType switch
            {
                _ when string.Equals(allocationType, AllocationTypeEnum.Agency.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.ValidateAgency.ToString(),
                _ when string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.ValidateTelecallingAgency.ToString(),
                _ when string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.ValidateAllocationOwner.ToString(),
                _ => string.Empty
            };
        }
        /// <summary>
        /// Gets the allocation stored procedure name based on the allocation type.
        /// </summary>
        /// <param name="allocationType">The type of allocation.</param>
        /// <returns>The name of the allocation stored procedure.</returns>
        private string GetAllocationStoredProcedure(string allocationType)
        {
            return allocationType switch
            {
                _ when string.Equals(allocationType, AllocationTypeEnum.Agency.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.AllocateToAgency.ToString(),
                _ when string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.AllocateToTelecallingAgency.ToString(),
                _ when string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value, StringComparison.OrdinalIgnoreCase) => PrimaryAllocationStoredProcedureEnum.AllocateToOwner.ToString(),
                _ => string.Empty
            };
        }

        /// <summary>
        /// Generates a CSV report for the allocation process.
        /// </summary>
        private void GenerateCsvReportAsync()
        {
            _logger.LogInformation("ProcessPrimaryAllocationFile: Generating CSV Report");

            var obj = new GenerateCsvFile
            {
                StoredProcedure = PrimaryAllocationStoredProcedureEnum.ExportToCsv.Value,
                WorkRequestId = _model.CustomId,
                ActionType = _allocationByType,
                TenantId = _tenantId,
                Destination = _fileSystem.Path.Combine(_destinationPath, _model.CustomId.ToString())
            };
            _logger.LogInformation($"ProcessPrimaryAllocationFile: Generating CSV with details: {JsonConvert.SerializeObject(obj)}");
            _csvExcelUtility.GenerateCsvFile(obj);
        }
        /// <summary>
        /// Returns the destination folder path based on the given file status string.
        /// Matches the status against defined FileStatusEnum values.
        /// </summary>
        /// <param name="status">The file status string.</param>
        /// <returns>The destination folder path corresponding to the given file status.</returns>
        private string GetProcessedPath(string status)
        {
            if (string.Equals(status, FileStatusEnum.Processed.Value, StringComparison.OrdinalIgnoreCase))
                return _successProcessedPath;
            else if (string.Equals(status, FileStatusEnum.Failed.Value, StringComparison.OrdinalIgnoreCase))
                return _failedProcessedPath;
            else if (string.Equals(status, FileStatusEnum.Partial.Value, StringComparison.OrdinalIgnoreCase))
                return _partialProcessedPath;
            else if (string.Equals(status, FileStatusEnum.InvalidFileFormat.Value, StringComparison.OrdinalIgnoreCase))
                return _invalidProcessedPath;
            else if (string.Equals(status, FileStatusEnum.Error.Value, StringComparison.OrdinalIgnoreCase))
                return _errorProcessedPath;
            else
                return string.Empty; // Or fallback
        }

    }
}
