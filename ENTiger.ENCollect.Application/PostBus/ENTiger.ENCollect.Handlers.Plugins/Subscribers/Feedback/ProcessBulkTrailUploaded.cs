using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.DomainModels.Utilities;
using ENTiger.ENCollect.Utilities.SSISPackage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Data;
using System.Data.OleDb;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessBulkTrailUploaded : IProcessBulkTrailUploaded
    {
        protected readonly ILogger<ProcessBulkTrailUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IFlexHost _flexHost;
        string? loggedInUserId;
        protected BulkTrailUploadFile? _model;
        protected RunStatus _runStatusModel;
        protected readonly PackageSSISProviderFactory _packageSSISProviderFactory;
        string destinationPath = string.Empty;
        private string _successProcessedPath;
        private string _failedProcessedPath;
        private string _partialProcessedPath;
        private string _invalidProcessedPath;

        string DBType = string.Empty;
        private string _tenantId;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly IFileSystem _fileSystem;
        private readonly MicrosoftSettings _microsoftSettings;
        private readonly IFileTransferUtility _fileTransferUtility;
        public ProcessBulkTrailUploaded(ILogger<ProcessBulkTrailUploaded> logger, 
            IRepoFactory repoFactory
            , PackageSSISProviderFactory packageSSISProviderFactory, 
            IFlexHost flexHost, 
            ICsvExcelUtility csvExcelUtility, 
            IOptions<FilePathSettings> fileSettings, 
            IOptions<DatabaseSettings> databaseSettings, 
            IFileSystem fileSystem, 
            IOptions<MicrosoftSettings> microsoftSettings,
             IFileTransferUtility fileTransferUtility
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _packageSSISProviderFactory = packageSSISProviderFactory;
            _flexHost = flexHost;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _fileSystem = fileSystem;
            _microsoftSettings = microsoftSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }

        public virtual async Task Execute(BulkTrailUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            int commandTimeout = Convert.ToInt32(AppConfigManager.GetSection("FlexBase")["CommandTimeout"] ?? "60");
            string rootFolderPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            destinationPath = _fileSystem.Path.Combine(rootFolderPath, _fileSettings.BulkTrailProcessedFilePath);
            _successProcessedPath = _fileSystem.Path.Combine(destinationPath, _fileSettings.SuccessProcessedFilePath);
            _failedProcessedPath = _fileSystem.Path.Combine(destinationPath, _fileSettings.FailedProcessedFilePath);
            _partialProcessedPath = _fileSystem.Path.Combine(destinationPath, _fileSettings.PartialProcessedFilePath);
            _invalidProcessedPath = _fileSystem.Path.Combine(destinationPath, _fileSettings.InvalidProcessedFilePath);
            string FileFolderPath = rootFolderPath;
            string tempFolderPath = _fileSystem.Path.Combine(rootFolderPath, _fileSettings.TemporaryPath);
            DBType = _databaseSettings.DBType;
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            _tenantId = _flexAppContext.TenantId;

            _model = await _repoFactory.GetRepo().FindAll<BulkTrailUploadFile>().Where(a => a.CustomId == @event.CustomId).FirstOrDefaultAsync();

            try
            {
                if (_model != null)
                {
                    BulkTrailUploadCommand command = new BulkTrailUploadCommand();
                    command.CustomId = _model.CustomId;

                    if (string.Equals(DBType, DBTypeEnum.MySQL.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        string? sqlConnectionString = await _repoTenantFactory.FindAll<FlexTenantBridge>()
                                                                .Where(x => x.Id == _flexAppContext.TenantId)
                                                                .Select(x => x.DefaultWriteDbConnectionString)
                                                                .FirstOrDefaultAsync();

                        _logger.LogInformation("BulkTrailUploadedEventHandler : customid - " + _model.CustomId);

                        string MyConnectionString;
                        string fileName;
                        _logger.LogInformation("BulkTrailUploadedEventHandler : Fetch MyConnectionString");

                        MyConnectionString = sqlConnectionString;

                        _logger.LogInformation("BulkTrailUploadedEventHandler : MyConnectionString - " + MyConnectionString);

                        MySqlConnection connection = new MySqlConnection(MyConnectionString);
                        MySqlCommand cmd = connection.CreateCommand();
                        connection.Open();
                        int numberOfInsertedRows = 0;
                        try
                        {
                            ConvertExcelToCsvNew(rootFolderPath, tempFolderPath, _model.CustomId, _model.FileName.Replace(".xlsx", ""));
                            fileName = _fileSystem.Path.Combine(tempFolderPath, _model.FileName.Replace("xlsx", "csv"));
                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step1 - Insert into Intermediatetable : TrailBulkUploadIntermediateTable");
                            using (var conn = new MySqlConnection(MyConnectionString))
                            {
                                var blc = new MySqlBulkLoader(conn)
                                {
                                    TableName = "TrailBulkUploadIntermediateTable",
                                    Timeout = 600,
                                    FieldTerminator = ",",
                                    LineTerminator = "\n",
                                    FileName = fileName,
                                    NumberOfLinesToSkip = 1,
                                    Columns = { "AccountNo", "AgentCode", "RightPartyContact", "DispositionCodeGroup", "DispositionCode", "PTPDate", "PTPAmount", "NextActionCode", "Remarks", "NewContactNumber", "NewArea", "NewAddress", "NewEmailID", "AddressType", "PickupAddress", "WorkRequestId", "Id", "CreatedDate", "LastModifiedDate" }
                                };
                                numberOfInsertedRows = blc.Load();
                            }
                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step1 : Data inserted into intermediatetable via MySqlBulkLoader");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in BulkTrailUploadedEventHandler : Invalid File Format - " + ex + ex?.InnerException);

                            _model.Status = "Invalid File Format";
                            _model.SetAddedOrModified();
                            _repoFactory.GetRepo().InsertOrUpdate(_model);
                            await _repoFactory.GetRepo().SaveAsync();
                            await _fileTransferUtility.RenameAndMoveFileAsync(rootFolderPath, _invalidProcessedPath, _model.FileName);
                            _logger.LogInformation("BulkTrailUploadedEventHandler : BulkTrailFailedEvent Published");
                            throw;
                        }
                        try
                        {
                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step2 - Execute SP : TrailBulkUploadValidations");
                            using (MySqlCommand cmdSPTruncateIntermediateBulkTrailImport = new MySqlCommand("TrailBulkUploadValidations", connection))
                            {
                                cmdSPTruncateIntermediateBulkTrailImport.CommandType = CommandType.StoredProcedure;
                                cmdSPTruncateIntermediateBulkTrailImport.Parameters.AddWithValue("@p_WorkRequestId", _model.CustomId);
                                // Set command timeout from configuration
                                cmdSPTruncateIntermediateBulkTrailImport.CommandTimeout = commandTimeout;
                                cmdSPTruncateIntermediateBulkTrailImport.ExecuteNonQuery();
                            }

                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step3 - Execute SP : TrailBulkInsertFeedBack");
                            using (MySqlCommand cmdTrailBulkInsertFeedBack = new MySqlCommand("TrailBulkInsertFeedBack", connection))
                            {
                                cmdTrailBulkInsertFeedBack.CommandType = CommandType.StoredProcedure;
                                cmdTrailBulkInsertFeedBack.Parameters.AddWithValue("@p_WorkRequestId", _model.CustomId);
                                cmdTrailBulkInsertFeedBack.Parameters.AddWithValue("@p_TransactionSource", TransactionSourceEnum.BulkUpload.Value);
                                // Set command timeout from configuration
                                cmdTrailBulkInsertFeedBack.CommandTimeout = commandTimeout;
                                cmdTrailBulkInsertFeedBack.ExecuteNonQuery();
                            }

                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step4 - Execute SP : TrailBulkUploadFileStatus");
                            using (MySqlCommand cmdTrailBulkUploadFileStatus = new MySqlCommand("TrailBulkUploadFileStatus", connection))
                            {
                                cmdTrailBulkUploadFileStatus.CommandType = CommandType.StoredProcedure;
                                cmdTrailBulkUploadFileStatus.Parameters.AddWithValue("@p_WorkRequestId", _model.CustomId);
                                // Set command timeout from configuration
                                cmdTrailBulkUploadFileStatus.CommandTimeout = commandTimeout;
                                cmdTrailBulkUploadFileStatus.ExecuteNonQuery();
                            }

                            _logger.LogInformation("BulkTrailUploadedEventHandler : DestinationPath - " + destinationPath);

                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step5 - GenerateCsvFile | Execute Data SP : GetTrailBulkUploadDetails");
                            GenerateCsvFile obj = new GenerateCsvFile();

                            obj.StoredProcedure = "GetTrailBulkUploadDetails";
                            obj.WorkRequestId = _model.CustomId;
                            obj.Destination = destinationPath + "\\" + _model.CustomId;
                            obj.TenantId = _tenantId;
                            _logger.LogInformation("BulkTrailUploadedEventHandler : JSON - " + JsonConvert.SerializeObject(obj));
                            _csvExcelUtility.GenerateCsvFile(obj);

                            _logger.LogInformation("BulkTrailUploadedEventHandler : Step6 - Execute SP : TrailBulkUploadRecordsCleanUp");
                            using (MySqlCommand cmdTrailBulkUploadRecordsCleanUp = new MySqlCommand("TrailBulkUploadRecordsCleanUp ", connection))
                            {
                                cmdTrailBulkUploadRecordsCleanUp.CommandType = CommandType.StoredProcedure;
                                cmdTrailBulkUploadRecordsCleanUp.Parameters.AddWithValue("@p_WorkRequestId", _model.CustomId);
                                // Set command timeout from configuration
                                cmdTrailBulkUploadRecordsCleanUp.CommandTimeout = commandTimeout;
                                cmdTrailBulkUploadRecordsCleanUp.ExecuteNonQuery();
                            }
                            await _fileTransferUtility.RenameAndMoveFileAsync(rootFolderPath, _successProcessedPath, _model.FileName);
                            _logger.LogInformation("BulkTrailUploadedEventHandler : BulkTrailSucceededEvent Published");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(" - Exception in BulkTrailUploadedEventHandler : File Processing - " + ex + ex?.InnerException);
                            _model.Status = FileStatusEnum.Failed.Value;
                            _model.SetAddedOrModified();
                            _repoFactory.GetRepo().InsertOrUpdate(_model);
                            await _repoFactory.GetRepo().SaveAsync();
                            await _fileTransferUtility.RenameAndMoveFileAsync(rootFolderPath, _failedProcessedPath, _model.FileName);
                            _logger.LogInformation("BulkTrailUploadedEventHandler : BulkTrailFailedEvent Published");
                        }
                    }
                    else
                    {
                        string status = await ProcessSSISPackage(@event);
                        EventCondition = string.IsNullOrEmpty(status) || string.Equals(status, "failure", StringComparison.OrdinalIgnoreCase) ? CONDITION_ONFAILURE : CONDITION_ONSUCCESS;
                    }
                }

                EventCondition = CONDITION_ONSUCCESS;
            }
            catch (Exception ex)
            {
                _model.Status = FileStatusEnum.Failed.Value;
                _model.SetAddedOrModified();
                _repoFactory.GetRepo().InsertOrUpdate(_model);
                await _repoFactory.GetRepo().SaveAsync();
                await _fileTransferUtility.RenameAndMoveFileAsync(rootFolderPath, destinationPath, _model.FileName);
                EventCondition = CONDITION_ONFAILURE;
            }

            await this.Fire<ProcessBulkTrailUploaded>(EventCondition, serviceBusContext);
        }

        private async Task<string> ProcessSSISPackage(BulkTrailUploadedEvent @event)
        {
            BulkUploadFileDto dto = new BulkUploadFileDto
            {
                CustomId = @event.CustomId,
                FileType = "BulkTrailUpload",
                FileName = @event.FileName
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

            SSISResultDto ssisResultDto = await ssisPackage.ExecutePackage(dto, @event.Id);
            result = ssisResultDto?.PackageExecResult;

            //Update Finish Run
            _logger.LogInformation("Runstatus Completed :" + dto.CustomId);
            _runStatusModel.FinishRun();
            // SaveUpdateModel();
            _repoFactory.GetRepo().InsertOrUpdate(_runStatusModel);
            await _repoFactory.GetRepo().SaveAsync();
            return result;
        }


        public void ConvertExcelToCsvNew(string source, string destination, string WorkRequestId, string Filename)
        {
            string provider = _microsoftSettings.ExcelProvider;
            _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsvNew - Start | Source - " + source + " | Destination" + destination);
            try
            {
                string SourceFolderPath = source;
                string DestinationFolderPath = destination;
                string FileDelimited = @",";
                string fileFullPath = "";
                fileFullPath = SourceFolderPath + "\\" + Filename + ".xlsx";
                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsvNew - FileFullPath : " + fileFullPath);

                //Create Excel Connection
                string ConStr;
                string HDR = "YES";
                //ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullPath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                ConStr = $"Provider={provider};Data Source={fileFullPath};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsv - Open Connection : " + ConStr);
                OleDbConnection cnn = new OleDbConnection(ConStr);

                //Get Sheet Names
                cnn.Open();
                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsv - Connection Opened Successfully");
                DataTable dtSheet = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //Load the DataTable with Sheet Data
                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsv - Query : select * from [Sheet1$]");

                string whereClause = $"WHERE [{ExcelFilterTypeEnum.BulkTrailUpload.Value}] IS NOT NULL AND [{ExcelFilterTypeEnum.BulkTrailUpload.Value}] <> ''";
                string query = $"SELECT * FROM [Sheet1$] {whereClause}";
                OleDbCommand oconn = new OleDbCommand(query, cnn);
                OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //Create CSV File and load data to it from Sheet
                StreamWriter sw = new StreamWriter(DestinationFolderPath + "\\" + Filename + ".csv", false);
                int ColumnCount = dt.Columns.Count;
                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsv - ColumnCount : " + ColumnCount);
                // Write the Header Row to File
                for (int i = 0; i < ColumnCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < ColumnCount - 1)
                    {
                        sw.Write(FileDelimited);
                    }
                }
                sw.Write(FileDelimited);
                sw.Write("WorkRequestId");
                sw.Write(FileDelimited);
                sw.Write("Id");
                sw.Write(FileDelimited);
                sw.Write("CreatedDate");
                sw.Write(FileDelimited);
                sw.Write("LastModifiedDate");
                sw.Write(sw.NewLine);
                _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsv - RowsCount : " + dt.Rows.Count);
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            if (i == 6)
                            {
                                if (dr[i] != null)
                                {
                                    if (dr[i].ToString() != "")
                                    {
                                        sw.Write(Convert.ToDecimal(dr[i]));
                                    }
                                    else
                                    {
                                        sw.Write(0.0);
                                    }
                                }
                            }
                            else
                            {
                                if (i != 5)
                                {
                                    sw.Write(dr[i]?.ToString());
                                }
                                else if (!string.IsNullOrWhiteSpace(Convert.ToString(dr[i])) && DateTime.TryParse(Convert.ToString(dr[i]), out DateTime validDate))
                                {
                                    sw.Write(validDate.ToString("yyyy-MM-dd"));
                                }
                            }
                        }
                        if (i < ColumnCount - 1)
                        {
                            sw.Write(FileDelimited);
                        }
                    }
                    sw.Write(FileDelimited);
                    sw.Write(WorkRequestId);
                    sw.Write(FileDelimited);
                    sw.Write(SequentialGuid.NewGuidString());
                    sw.Write(FileDelimited);
                    sw.Write(Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd h:mm:ss"));
                    sw.Write(FileDelimited);
                    sw.Write(Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd h:mm:ss"));
                    sw.Write(sw.NewLine);
                }
                sw.Close();
                cnn.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in BulkTrailUploadedEventHandler : ConvertExcelToCsvNew - " + ex + ex?.Message + ex?.StackTrace + ex?.InnerException);
                throw;
            }
            _logger.LogInformation("BulkTrailUploadedEventHandler : ConvertExcelToCsvNew - End");
        }
    }
}