using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;
using ENTiger.ENCollect.DomainModels.Utilities;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionBulkUploaded : IProcessCollectionBulkUploaded
    {
        protected readonly ILogger<ProcessCollectionBulkUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly DatabaseSettings _databaseSettings;
        protected RunStatus _runStatusModel;
        protected readonly PackageSSISProviderFactory _packageSSISProviderFactory;
        protected readonly IFlexHost _flexHost;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _fileSettings;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileTransferUtility _fileTransferUtility;
        protected CollectionUploadFile? _model;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        private IDbUtility _dbUtility;

        string _dbType = string.Empty;
        private string _tempFolderPath;
        private string _rootFolderPath;
        private string _destinationPath;
        private char _deliminator;
        private string _tenantId;

        private string _successProcessedPath;
        private string _failedProcessedPath;
        private string _partialProcessedPath;
        private string _invalidProcessedPath;

        public ProcessCollectionBulkUploaded(ILogger<ProcessCollectionBulkUploaded> logger,
            IRepoFactory repoFactory,
            IOptions<DatabaseSettings> databaseSettings,
            IOptions<FilePathSettings> fileSettings,
            PackageSSISProviderFactory packageSSISProviderFactory,
            IFlexHost flexHost,
            IFileSystem fileSystem,
            ICsvExcelUtility csvExcelUtility,
            IOptions<FileConfigurationSettings> fileConfigurationSettings
            , IFileTransferUtility fileTransferUtility

           )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _databaseSettings = databaseSettings.Value;
            _fileSettings = fileSettings.Value;
            _packageSSISProviderFactory = packageSSISProviderFactory;
            _flexHost = flexHost;
            _fileSystem = fileSystem;
            _csvExcelUtility = csvExcelUtility;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }

        public virtual async Task Execute(CollectionBulkUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            var repo = _repoFactory.Init(@event);

            //Set variables
            SetVariables();

            //configure
            ConfigreDBUtilityInstance();

            _model = await _repoFactory.GetRepo().FindAll<CollectionUploadFile>()
                .Where(a => a.CustomId == @event.CustomId).FirstOrDefaultAsync();

            try
            {
                if (_dbType.ToLower() == DBTypeEnum.MySQL.Value.ToLower())
                {
                    await ProcessBulkUpload();
                    EventCondition = CONDITION_ONSUCCESS;
                }
                else if (_dbType.ToLower() == DBTypeEnum.MsSQL.Value.ToLower())
                {
                    string status = await ProcessSSISPackage(@event);
                    EventCondition = string.IsNullOrEmpty(status) || status.ToLower() == "failure" ? CONDITION_ONFAILURE : CONDITION_ONSUCCESS;
                }
            }
            catch (Exception ex)
            {
                await UpdateFileStatus(FileStatusEnum.Failed.Value);
                EventCondition = CONDITION_ONFAILURE;
            }

            await this.Fire<ProcessCollectionBulkUploaded>(EventCondition, serviceBusContext);
        }

        private void SetVariables()
        {
            _tenantId = _flexAppContext.TenantId;
            _deliminator = Convert.ToChar(_fileConfigurationSettings.Delimiter);
            _dbType = _databaseSettings.DBType;
            _rootFolderPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _tempFolderPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.TemporaryPath);
            _destinationPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.BulkCollectionProcessedFilePath);
            _successProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.SuccessProcessedFilePath);
            _failedProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.FailedProcessedFilePath);
            _partialProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.PartialProcessedFilePath);
            _invalidProcessedPath = _fileSystem.Path.Combine(_destinationPath, _fileSettings.InvalidProcessedFilePath);
        }

        private void ConfigreDBUtilityInstance()
        {
            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(_flexAppContext.HostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(_dbType.ToLower());
            _dbUtility = utility.GetUtility(dbUtilityEnum);
        }

        private async Task<string> ProcessSSISPackage(CollectionBulkUploadedEvent @event)
        {
            var dto = CreateBulkUploadFileDto(@event);
            LogInputJson(dto);

            // Start Run Status
            await StartRunStatus(dto);

            // Process SSIS Package
            var result = await ExecuteSSISPackage(dto);

            // Complete Run Status
            await CompleteRunStatus();

            return result;
        }
        private BulkUploadFileDto CreateBulkUploadFileDto(CollectionBulkUploadedEvent @event)
        {
            var dto = new BulkUploadFileDto
            {
                CustomId = @event.CustomId,
                FileType = UploadTypeEnum.CollectionBulkUpload.Value,
                FileName = _model.FileName
            };
            dto.SetAppContext(_flexAppContext);
            return dto;
        }
        private void LogInputJson(BulkUploadFileDto dto)
        {
            _logger.LogInformation("Input Json: {Json}", JsonConvert.SerializeObject(dto));
        }

        private async Task StartRunStatus(BulkUploadFileDto dto)
        {
            string userId = dto.GetAppContext().UserId;

            _logger.LogInformation("Run status saving: {CustomId}", dto.CustomId);
            _runStatusModel = _flexHost.GetDomainModel<RunStatus>().StartRun(dto.FileType, dto.CustomId, userId);

            await SaveRunStatus();
        }
        private async Task<string> ExecuteSSISPackage(BulkUploadFileDto dto)
        {
            var ssisPackageProvider = _packageSSISProviderFactory.GetSSISPackageProvider(dto.FileType);
            var ssisResult = await ssisPackageProvider.ExecutePackage(dto, _model.Id);
            return ssisResult?.PackageExecResult ?? string.Empty;
        }

        private async Task CompleteRunStatus()
        {
            _logger.LogInformation("Run status completed: {CustomId}", _runStatusModel.CustomId);
            _runStatusModel.FinishRun();
            await SaveRunStatus();
        }

        private async Task SaveRunStatus()
        {
            _repoFactory.GetRepo().InsertOrUpdate(_runStatusModel);
            await _repoFactory.GetRepo().SaveAsync();
        }
        private async Task ProcessBulkUpload()
        {
            _logger.LogInformation($"ProcessCollectionBulkUploaded: Processing WorkRequestId {_model.CustomId}");
            try
            {
                string csvFilePath = ConvertToCsv();
                int inserted = await BulkInsertIntoIntermediateTable(csvFilePath);

                if (inserted > 0)
                {
                    await ExecuteStoredProcedures();
                }

                _logger.LogInformation("ProcessCollectionBulkUploaded: Bulk Upload Processing Completed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Error] ProcessCollectionBulkUploaded Failed while processing WorkRequestId: {_model.CustomId}. Error: {ex.Message}");
                await UpdateFileStatus(FileStatusEnum.Failed.Value);
            }
        }


        private string ConvertToCsv()
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Converting Excel to CSV");
            // Check the file extension using a case-insensitive comparison.
            if (!string.Equals(Path.GetExtension(_model.FileName), FileTypeEnum.CSV.Value, StringComparison.OrdinalIgnoreCase))
            {
                // Change ".xlsx" to ".csv" for the output file name.
                string csvFileName = _model.FileName.Replace(FileTypeEnum.XLSX.Value, FileTypeEnum.CSV.Value);
                string csvFilePath = _fileSystem.Path.Combine(_tempFolderPath, csvFileName);
                List<string> headers=new List<string>();

                // Call conversion only if the file is not already a CSV.
                _csvExcelUtility.ConvertExcelToCsv(_rootFolderPath, _tempFolderPath, _model.CustomId, _model.FileName.Replace(FileTypeEnum.XLSX.Value, ""), ExcelFilterTypeEnum.CollectionUpload.Value, headers);
                return csvFilePath;
            }
            else
            {
                // If the file is already CSV, return the existing file path.
                return _fileSystem.Path.Combine(_tempFolderPath, _model.FileName);
            }
        }
        private async Task<int> BulkInsertIntoIntermediateTable(string filePath)
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Inserting into Intermediate Table");
            InsertIntermediateTableRequestDto request = new InsertIntermediateTableRequestDto
            {
                Delimiter = _deliminator.ToString(),
                TableName = CollectionStoredProcedureEnum.IntermediateTable.Value,
                FileName = filePath,
                TenantId = _tenantId
            };
            return await _dbUtility.InsertRecordsIntoIntermediateTable(request);
        }

        private async Task ExecuteStoredProcedures()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("p_TransactionSource", _flexAppContext?.RequestSource);
            parameters.Add("WorkRequestId", _model.CustomId);

            _logger.LogInformation("ProcessCollectionBulkUploaded: Executing Stored Procedures");

            request.SpName = CollectionStoredProcedureEnum.Validation.Value;
            request.TenantId = _tenantId;
            //Call the SP
            request.Parameters = parameters;
            await _dbUtility.ExecuteSP(request);

            request.SpName = CollectionStoredProcedureEnum.Insert.Value;
            await _dbUtility.ExecuteSP(request);

            request.SpName = CollectionStoredProcedureEnum.UpdateFileStatus.Value;
            await _dbUtility.ExecuteSP(request);

            GenerateCsvReport();

            request.SpName = CollectionStoredProcedureEnum.RecordsCleanUp.Value;
            await _dbUtility.ExecuteSP(request);

            // Move the processed file from the incoming path to the unallocation processed folder, overwriting if the file already exists
            await _fileTransferUtility.RenameAndMoveFileAsync(_rootFolderPath, _successProcessedPath, _model.FileName);
        }

        private void GenerateCsvReport()
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Generating CSV Report");

            var obj = new GenerateCsvFile
            {
                StoredProcedure = CollectionStoredProcedureEnum.UploadDetails.Value,
                WorkRequestId = _model.CustomId,
                TenantId = _tenantId,
                Destination = _fileSystem.Path.Combine(_destinationPath, _model.CustomId.ToString())
            };

            _logger.LogInformation($"ProcessCollectionBulkUploaded: Generating CSV with details: {JsonConvert.SerializeObject(obj)}");
            _csvExcelUtility.GenerateCsvFile(obj);
        }

        /// <summary>
        /// Updates the file processing status in the database.
        /// </summary>
        /// <param name="status">The new status to be set for the file.</param>
        private async Task UpdateFileStatus(string status)
        {
            _model.Status = status;
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
