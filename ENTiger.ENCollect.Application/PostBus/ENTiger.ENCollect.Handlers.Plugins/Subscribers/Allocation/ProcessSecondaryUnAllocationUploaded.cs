using System.Data;
using System.IO.Abstractions;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// Processes the secondary unallocation uploaded file.
    /// </summary>
    public partial class ProcessSecondaryUnAllocationUploaded : IProcessSecondaryUnAllocationUploaded
    {
        private readonly ILogger<ProcessSecondaryUnAllocationUploaded> _logger;
        private readonly IRepoFactory _repoFactory;
        private readonly IFlexHost _flexHost;
        private readonly IDataTableUtility _dataTableUtility;
        private readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _fileSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileTransferUtility _fileTransferUtility;

        private FlexAppContextBridge? _flexAppContext;
        private string _unAllocationType = string.Empty;
        private string _tenantId = string.Empty;
        private string _dbType = string.Empty;
        private string _hostName = string.Empty;
        private string _eventCondition = string.Empty;
        private string _tempFolderPath;
        private string _rootFolderPath;
        private string _destinationPath;
        private string _successProcessedPath;
        private string _failedProcessedPath;
        private string _partialProcessedPath;
        private string _invalidProcessedPath;

        private SecondaryUnAllocationFile? _model;
        private ApplicationUser? _user;
        private IDbUtility _dbUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessSecondaryUnAllocationUploaded"/> class.
        /// </summary>
        public ProcessSecondaryUnAllocationUploaded(
            ILogger<ProcessSecondaryUnAllocationUploaded> logger,
            IRepoFactory repoFactory,
            IFlexTenantRepository<FlexTenantBridge> repoTenantFactory,
            IDataTableUtility dataTableUtility,
            IFlexHost flexHost,
            IOptions<FilePathSettings> fileSettings,
            IOptions<DatabaseSettings> databaseSettings,
            IFileSystem fileSystem,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
             IFileTransferUtility fileTransferUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _repoTenantFactory = repoTenantFactory;
            _dataTableUtility = dataTableUtility;
            _flexHost = flexHost;
            _fileSystem = fileSystem;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }

        /// <summary>
        /// Executes the processing of a secondary unallocation file.
        /// </summary>
        /// <param name="eventData">Event data containing file details.</param>
        /// <param name="serviceBusContext">Service bus context.</param>
        public async Task Execute(SecondaryUnAllocationUploadedEvent eventData, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation($"Process started for SecondaryUnAllocationUploaded: {eventData.Id}");
            _flexAppContext = eventData.AppContext;

            var repo = _repoFactory.Init(eventData);
            var dbType = _databaseSettings.DBType;
            _unAllocationType = eventData.UnAllocationType.Replace(" ", "").Trim().ToLower();
            SetVariables(eventData);
            ConfigureDBUtilityInstance();

            _model = await _repoFactory.GetRepo().FindAll<SecondaryUnAllocationFile>()
                                .FirstOrDefaultAsync(x => x.Id == eventData.Id);

            _model.CustomId = _model.CustomId;

            _user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                               .FirstOrDefaultAsync(x => x.Id == _model.CreatedBy);

            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            string sheetName = _fileConfigurationSettings.DefaultSheet;
            var dataTable = _dataTableUtility.ExcelToDataTable(filePath, _model.FileName, sheetName);

            if (_dataTableUtility.ValidateUnAllocationHeaders(dataTable, _unAllocationType))
            {
                bool isSuccess = await ProcessValidFileAsync(dataTable, _model, eventData);
                _eventCondition = isSuccess ? CONDITION_ONSUCCESS : CONDITION_ONFAILURE;
            }
            else
            {
                await UpdateFileStatus(FileStatusEnum.InvalidFileFormat.Value);
                _eventCondition = CONDITION_ONFAILURE;
            }

            _logger.LogInformation($"Process completed for EventId: {eventData.Id}, File: {_model.FileName} with Condition: {_eventCondition}");
            await this.Fire<ProcessSecondaryUnAllocationUploaded>(_eventCondition, serviceBusContext);
        }

        /// <summary>
        /// Configures the database utility instance.
        /// </summary>
        private void ConfigureDBUtilityInstance()
        {
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(_flexAppContext.HostName);
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(_dbType.ToLower());
            _dbUtility = utility.GetUtility(dbUtilityEnum);
        }
        /// <summary>
        /// Sets necessary variables required for allocation file processing.
        /// </summary>
        /// <param name="eventData">Uploaded file event data.</param>
        private void SetVariables(SecondaryUnAllocationUploadedEvent eventData)
        {
            _logger.LogInformation($"SecondaryUnAllocationUploaded: Starting allocation file processing | DBType - {_databaseSettings.DBType}");
            _tenantId = _flexAppContext.TenantId;
            _dbType = _databaseSettings.DBType ?? string.Empty;

            _rootFolderPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _tempFolderPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.TemporaryPath);
            _destinationPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.UnAllocationProcessedFilePath);
            _successProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.SuccessProcessedFilePath);
            _failedProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.FailedProcessedFilePath);
            _partialProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.PartialProcessedFilePath);
            _invalidProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.InvalidProcessedFilePath);
        }

        /// <summary>
        /// Processes a valid unallocation file.
        /// </summary>
        private async Task<bool> ProcessValidFileAsync(DataTable dataTable, SecondaryUnAllocationFile model, SecondaryUnAllocationUploadedEvent @event)
        {
            _logger.LogInformation($"[Start] SecondaryUnAllocationFile Processing valid file for WorkRequestId: {model.CustomId}");

            try
            {
                await InsertIntoIntermediateTableAsync(dataTable, model);
                await ExecuteStoredProcedures(model);

                _logger.LogInformation($"[Success] SecondaryUnAllocationFile Completed processing for WorkRequestId: {model.CustomId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Error] SecondaryUnAllocationFile Failed while processing WorkRequestId: {model.CustomId}. Error: {ex.Message}");
                await UpdateFileStatus(FileStatusEnum.Failed.Value);
                return false;
            }
        }

        /// <summary>
        /// Inserts data into the intermediate table.
        /// </summary>
        private async Task InsertIntoIntermediateTableAsync(DataTable dataTable, SecondaryUnAllocationFile model)
        {
            var insertDto = new InsertIntoUnAllocationIntermediateTableRequestDto
            {
                WorkRequestId = model.CustomId,
                UserId = _flexAppContext.UserId,
                TenantId = _tenantId,
                Table = dataTable,
                TableName = SecondaryUnAllocationStoredProcedureEnum.InsertIntermediateTable.Value
            };

            _logger.LogInformation($"Inserting data into {insertDto.TableName} for File: {model.FileName}, WorkRequestId: {insertDto.WorkRequestId}");
            await _dbUtility.InsertIntoUnAllocationIntermediateTable(insertDto);
        }

        /// <summary>
        /// Executes the necessary stored procedures.
        /// </summary>
        private async Task ExecuteStoredProcedures(SecondaryUnAllocationFile model)
        {
            var request = new ExecuteSpRequestDto
            {
                TenantId = _tenantId,
                Parameters = new Dictionary<string, string>
            {
                { "WorkRequestId", model.CustomId },
                { "UnAllocationType", _unAllocationType },
                { "Type", model.UploadType },
                { "UserId", _user.Id }
            }
            };

            // Log the parameters before executing SPs
            _logger.LogInformation($"Starting SP execution for File: {model.FileName}. Parameters => WorkRequestId: {model.CustomId}, UnAllocationType: {_unAllocationType}, Type: {model.UploadType}, UserId: {_user.Id}");

            string[] storedProcedures =
            {
                     SecondaryUnAllocationStoredProcedureEnum.ValidateAccount.Value,
                     SecondaryUnAllocationStoredProcedureEnum.UpdateFileStatus.Value,
                     SecondaryUnAllocationStoredProcedureEnum.Update.Value
            };

            foreach (var sp in storedProcedures)
            {
                request.SpName = sp;
                await _dbUtility.ExecuteSP(request);
                _logger.LogInformation($"Executed SP: {sp} for File: {model.FileName}, WorkRequestId: {model.CustomId}");
            }
            // Move the processed file from the incoming path to the unallocation processed folder, overwriting if the file already exists
            await _fileTransferUtility.RenameAndMoveFileAsync(_rootFolderPath, _successProcessedPath, _model.FileName);
        }


        /// <summary>
        /// Updates the file processing status in the database.
        /// </summary>
        /// <param name="status">The new status to be set for the file.</param>
        private async Task UpdateFileStatus(string status)
        {
            _model.UpdateStatus(status);
            _model.SetLastModifiedDate(DateTime.Now);
            _model.SetAddedOrModified();

            // Update the file status in the database
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            await _repoFactory.GetRepo().SaveAsync();

            // Move the processed file from the root folder path to the destination path, allowing overwrite if the destination file already exists
            var destinationPath = GetProcessedPath(status);
            await _fileTransferUtility.RenameAndMoveFileAsync(_rootFolderPath, destinationPath, _model.FileName);
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
            else
                return string.Empty; // Or fallback
        }
    }
}
