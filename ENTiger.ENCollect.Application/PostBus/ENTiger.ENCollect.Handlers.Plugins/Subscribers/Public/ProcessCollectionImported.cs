using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ENTiger.ENCollect.DomainModels.Utilities;
using System.IO.Abstractions;
using ENTiger.ENCollect.CollectionsModule;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Newtonsoft.Json;
using ENTiger.ENCollect.Utilities.SSISPackage;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ProcessCollectionImported : IProcessCollectionImported
    {
        protected readonly ILogger<ProcessCollectionImported> _logger;
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

        protected CollectionUploadFile? _model;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        private IDbUtility _dbUtility;
        private string _tenantId;
        string DBType = string.Empty;
        private string _tempFolderPath;
        private string _rootFolderPath;
        private string _destinationPath;
        public ProcessCollectionImported(ILogger<ProcessCollectionImported> logger,
            IRepoFactory repoFactory,
            IOptions<DatabaseSettings> databaseSettings,
            IOptions<FilePathSettings> fileSettings,
            PackageSSISProviderFactory packageSSISProviderFactory,
            IFlexHost flexHost,
            IFileSystem fileSystem,
            ICsvExcelUtility csvExcelUtility
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
        }



        public virtual async Task Execute(CollectionImportedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            _tenantId = _flexAppContext.TenantId;
            DBType = _databaseSettings.DBType;
            _rootFolderPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _tempFolderPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.TemporaryPath);
            _destinationPath = _fileSystem.Path.Combine(_rootFolderPath, _fileSettings.BulkTrailProcessedFilePath);

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(_flexAppContext.HostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(DBType.ToLower());
            _dbUtility = utility.GetUtility(dbUtilityEnum);

            _model = await _repoFactory.GetRepo().FindAll<CollectionUploadFile>()
                            .Where(a => a.CustomId == @event.CustomId).FirstOrDefaultAsync();

            try
            {
                if (_model != null)
                {
                    if (DBType.ToLower() == DBTypeEnum.MySQL.Value.ToLower())
                    {
                        await ProcessBulkUpload();

                    }
                    else if (DBType.ToLower() == DBTypeEnum.MsSQL.Value.ToLower())
                    {
                        string status = await ProcessSSISPackage(@event);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire<ProcessCollectionImported>(EventCondition, serviceBusContext);
        }


        private async Task<int> BulkInsertIntoIntermediateTable(string filePath)
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Inserting into Intermediate Table");
            InsertIntermediateTableRequestDto requestDto = new InsertIntermediateTableRequestDto()
            {
                TableName = "CollectionBulkUploadIntermediateTable",
                TenantId = _tenantId,
                Delimiter = ",",
                FileName = filePath
            };


            int rowsInserted = await _dbUtility.InsertRecordsIntoIntermediateTable(requestDto);
            _logger.LogInformation($"ProcessCollectionBulkUploaded: rows inserted into Intermediate Table");
            return rowsInserted;

        }
        private async Task ExecuteStoredProcedures()
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Executing Stored Procedures");

            request.SpName = "CollectionBulkUploadValidations";
            request.TenantId = _flexAppContext.TenantId;
            request.WorkRequestId = _model.CustomId;
            //Call the SP
            await _dbUtility.ExecuteSP(request);

            request.SpName = "CollectionBulkInsert";
            await _dbUtility.ExecuteSP(request);

            request.SpName = "CollectionBulkUploadFileStatus";
            await _dbUtility.ExecuteSP(request);

            request.SpName = "CollectionBulkUploadRecordsCleanUp";
            await _dbUtility.ExecuteSP(request);

        }
        private void GenerateCsvReport()
        {
            _logger.LogInformation("ProcessCollectionBulkUploaded: Generating CSV Report");

            var obj = new GenerateCsvFile
            {
                StoredProcedure = "GetCollectionBulkUploadDetails",
                WorkRequestId = _model.CustomId,
                TenantId = _tenantId,
                Destination = Path.Combine(_destinationPath, _model.CustomId.ToString())
            };

            _logger.LogInformation($"ProcessCollectionBulkUploaded: Generating CSV with details: {JsonConvert.SerializeObject(obj)}");
            _csvExcelUtility.GenerateCsvFile(obj);
        }
        private async Task HandleProcessingError(Exception ex)
        {
            _logger.LogError($"ProcessCollectionBulkUploaded: Exception - {ex.Message}", ex);

            _model.Status = "Invalid File Format";
            _model.SetAddedOrModified();
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            await _repoFactory.GetRepo().SaveAsync();

            _logger.LogInformation("ProcessCollectionBulkUploaded: BulkTrailFailedEvent Published");
        }
        private async Task ProcessBulkUpload()
        {
            _logger.LogInformation($"ProcessCollectionBulkUploaded: Processing WorkRequestId {_model.CustomId}");
            try
            {
                string csvFilePath = _model.FilePath;
                int insertedRows = await BulkInsertIntoIntermediateTable(csvFilePath);

                if (insertedRows > 0)
                {
                    await ExecuteStoredProcedures();
                    GenerateCsvReport();
                }

                _logger.LogInformation("ProcessCollectionBulkUploaded: Bulk Upload Processing Completed.");
            }
            catch (Exception ex)
            {
                await HandleProcessingError(ex);
            }
        }
        private async Task<string> ProcessSSISPackage(CollectionImportedEvent @event)
        {
            BulkUploadFileDto dto = new BulkUploadFileDto
            {
                CustomId = @event.CustomId,
                FileType = "CollectionImport",
                FileName = _model.FileName
            };
            dto.SetAppContext(_flexAppContext);
            string UserId = dto.GetAppContext().UserId;

            _logger.LogInformation("Input Json :" + JsonConvert.SerializeObject(dto));

            //Start Run
            _logger.LogInformation("Run status saving :" + dto.CustomId);
            _runStatusModel = _flexHost.GetDomainModel<RunStatus>().StartRun(dto.FileType, dto.CustomId, UserId);
            // SaveUpdateModel();
            _repoFactory.GetRepo().InsertOrUpdate(_runStatusModel);
            await _repoFactory.GetRepo().SaveAsync();
            //Process Package
            //Fetch the Primary Allocation Package

            var ssisPackage = _packageSSISProviderFactory.GetSSISPackageProvider(dto.FileType);
            string result = string.Empty;

            SSISResultDto ssisResultDto = await ssisPackage.ExecutePackage(dto, _model.Id);
            result = ssisResultDto?.PackageExecResult;

            //Update Finish Run
            _logger.LogInformation("Runstatus Completed :" + dto.CustomId);
            _runStatusModel.FinishRun();
            
            _repoFactory.GetRepo().InsertOrUpdate(_runStatusModel);
            await _repoFactory.GetRepo().SaveAsync();
            return result;
        }

    }
}
