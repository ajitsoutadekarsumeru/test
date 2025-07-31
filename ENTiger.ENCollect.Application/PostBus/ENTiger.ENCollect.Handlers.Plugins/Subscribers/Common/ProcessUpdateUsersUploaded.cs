using System.Data;
using System.IO.Abstractions;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessUpdateUsersUploaded : IProcessUpdateUsersUploaded
    {
        protected readonly ILogger<ProcessUpdateUsersUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        protected readonly IDataTableUtility _dataTableUtility;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string hostName = string.Empty;
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        string dbType = string.Empty;
        UsersUpdateFile? model;
        ApplicationUser? user;
        private readonly FilePathSettings _fileSettings;
        private string tempPath = string.Empty;
        private readonly DatabaseSettings _databaseSettings;
        private readonly IFileSystem _fileSystem;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileTransferUtility _fileTransferUtility;
        public ProcessUpdateUsersUploaded(
            ILogger<ProcessUpdateUsersUploaded> logger,
            IRepoFactory repoFactory,
            IFlexTenantRepository<FlexTenantBridge> repoTenantFactory,
            IOptions<FilePathSettings> fileSettings,
            IOptions<DatabaseSettings> databaseSettings,
            IFileSystem fileSystem,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IDataTableUtility dataTableUtility,
            IFlexHost flexHost,
             IFileTransferUtility fileTransferUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _repoTenantFactory = repoTenantFactory;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _dataTableUtility = dataTableUtility;
            _flexHost = flexHost;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }

        public virtual async Task Execute(UpdateUsersUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            tempPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath);
            string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            dbType = _databaseSettings.DBType;
            string _sheetName = _fileConfigurationSettings.DefaultSheet;
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.UserProcessedFilePath);
            _flexAppContext = @event.AppContext; //do not remove this line
            string userId = _flexAppContext.UserId;
            string tenantId = _flexAppContext.TenantId;
            var repo = _repoFactory.Init(@event);
            _logger.LogInformation("ProcessUpdateUsersUploaded : Start");
            _logger.LogInformation("ProcessUpdateUsersUploaded : JSON - " + JsonConvert.SerializeObject(@event));

            hostName = _flexAppContext.HostName;

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            IDbUtility dbUtility = utility.GetUtility(dbUtilityEnum);

            model = await _repoFactory.GetRepo().FindAll<UsersUpdateFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

            var dataTable = _dataTableUtility.ExcelToDataTable(_filepath, model.FileName, _sheetName);
            _logger.LogInformation($"ProcessUpdateUsersUploaded : DataTableCount - {dataTable.Rows?.Count ?? 0}");

            bool IsCorrectFileHeader = await ValidateUsersUpdateHeadersAsync(dataTable, model.UploadType);

            if (IsCorrectFileHeader)
            {
                //Step 1: Import to UsersUpdateIntermediate
                await InsertIntoIntermediateTableAsync("UsersUpdateIntermediate", dataTable, userId, model.CustomId, tenantId, model.UploadType);

                //Step 2: Users Update Validations
                var values = new Dictionary<string, string>();
                values.Add("WorkRequestId", model.CustomId);
                values.Add("Type", model.UploadType);
                values.Add("UserId", user.Id);

                //construct the request for executing the SP
                request.SpName = "UsersUpdate_Validation";
                request.TenantId = _flexAppContext.TenantId;
                request.Parameters = values;
                //Call the SP
                await dbUtility.ExecuteSP(request);

                //Step 3: Update File Status
                values = new Dictionary<string, string>();
                values.Add("WorkRequestId", model.CustomId);

                request.SpName = "UsersUpdate_UpdateFileStatus";
                request.Parameters = values;
                //Call the SP
                await dbUtility.ExecuteSP(request);

                //Step 4: Update details in MainTable
                values = new Dictionary<string, string>();
                values.Add("WorkRequestId", model.CustomId);
                values.Add("Type", model.UploadType);
                values.Add("UserId", user.Id);

                request.SpName = "UsersUpdate_Update";
                request.Parameters = values;
                //Call the SP
                await dbUtility.ExecuteSP(request);

                // Move the processed file from the incoming path to the user processed folder, overwriting if the file already exists
                await _fileTransferUtility.RenameAndMoveFileAsync(_filepath, destPath, model.FileName);
                //Step 7: Send Success Email
                //SendSuccessMail(model, AttachmentData, user);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                model.Status = "Invalid File Format";
                model.SetLastModifiedDate(DateTime.Now);
                model.SetAddedOrModified();

                _repoFactory.GetRepo().InsertOrUpdate(model);
                int records = await _repoFactory.GetRepo().SaveAsync();

                EventCondition = CONDITION_ONFAILURE;
            }
            _logger.LogInformation("ProcessUpdateUsersUploaded : End | EventCondition - " + EventCondition);
            await this.Fire<ProcessUpdateUsersUploaded>(EventCondition, serviceBusContext);
        }

        public async Task<bool> ValidateUsersUpdateHeadersAsync(DataTable dataTable, string type)
        {
            bool IsCorrectFileHeader = true;

            var dynamicHeaders = dataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName.ToLower()).ToArray();

            List<string> staticHeaders = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                                            .Where(x => string.Equals(x.CategoryMasterId, CategoryMasterEnum.UsersUpdateHeaders.Value) &&
                                                        string.Equals(x.Code, type))
                                            .OrderBy(x => x.Id)
                                            .Select(x => x.Name.ToLower()).ToListAsync();

            var result = staticHeaders.Where(x1 => !dynamicHeaders.Any(x2 => string.Equals(x1.Trim(), x2.Trim())))
                                      .Union(dynamicHeaders.Where(x1 => !staticHeaders.Any(x2 => string.Equals(x1.Trim(), x2.Trim()))));

            IsCorrectFileHeader = result.Count() > 0 ? false : true;

            return IsCorrectFileHeader;
        }


        public async Task InsertIntoIntermediateTableAsync(string table, DataTable data, string userId, string WorkRequestId, string tenantId, string type)
        {
            _logger.LogInformation("ProcessUpdateUsersUploaded : InsertIntoIntermediate - Start : DBType - " + dbType + " | WorkRequestId - " + WorkRequestId + " | Table - " + table);
            string? sqlConnectionString = await _repoTenantFactory.FindAll<FlexTenantBridge>()
                                                    .Where(x => x.Id == tenantId)
                                                    .Select(x => x.DefaultWriteDbConnectionString)
                                                    .FirstOrDefaultAsync();
            _logger.LogInformation("ProcessUpdateUsersUploaded : Type - " + type + " | TenantId - " + tenantId + " | ConnectionString - " + sqlConnectionString);
            try
            {
                var dt = new DataTable();

                if (string.Equals(dbType, DBTypeEnum.MsSQL.Value, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(type, "AGENT", StringComparison.OrdinalIgnoreCase))
                    {
                        dt.Columns.Add("AgencyCode", typeof(string));
                    }
                    dt.Columns.Add("UserCode", typeof(string));
                    dt.Columns.Add("Action", typeof(string));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("IsError", typeof(bool));
                    dt.Columns.Add("Reason", typeof(string));
                    dt.Columns.Add("WorkRequestId", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));

                    if (string.Equals(type, "AGENT", StringComparison.OrdinalIgnoreCase))
                    {
                        data.AsEnumerable()
                        //.Skip(1) // skip headers
                        //.Where(dr => Convert.ToString(dr[0]).Length > 0)
                        .ToList()
                        .ForEach(dr => dt.Rows.Add(dr[0], dr[1], dr[2], userId, DateTime.Now, false, string.Empty, WorkRequestId, dr[3]));
                    }
                    else
                    {
                        data.AsEnumerable()
                        //.Skip(1) // skip headers
                        // .Where(dr => Convert.ToString(dr[0]).Length > 0)
                        .ToList()
                        .ForEach(dr => dt.Rows.Add(dr[0], dr[1], userId, DateTime.Now, false, string.Empty, WorkRequestId, dr[2]));
                    }

                    using (var sqlCon = new SqlConnection(sqlConnectionString))
                    {
                        try
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon))
                            {
                                sqlBulkCopy.DestinationTableName = table;
                                if (string.Equals(type, "AGENT", StringComparison.OrdinalIgnoreCase))
                                {
                                    sqlBulkCopy.ColumnMappings.Add("AgencyCode", "AgencyCode");
                                }
                                sqlBulkCopy.ColumnMappings.Add("UserCode", "UserCode");
                                sqlBulkCopy.ColumnMappings.Add("Action", "Action");
                                sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                                sqlBulkCopy.ColumnMappings.Add("IsError", "IsError");
                                sqlBulkCopy.ColumnMappings.Add("Reason", "Reason");
                                sqlBulkCopy.ColumnMappings.Add("WorkRequestId", "WorkRequestId");
                                sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");

                                sqlCon.Open();
                                sqlBulkCopy.BulkCopyTimeout = 0;
                                sqlBulkCopy.WriteToServer(dt);
                                sqlCon.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in ProcessUpdateUsersUploaded : InsertIntoIntermediate - " + ex);
                            throw ex;
                        }
                    }
                }
                else
                {
                    dt.Columns.Add("Id", typeof(string));
                    dt.Columns.Add("WorkRequestId", typeof(string));
                    //if (type.ToLower() == "AGENT".ToLower())
                    {
                        dt.Columns.Add("AgencyCode", typeof(string));
                    }
                    dt.Columns.Add("UserCode", typeof(string));
                    dt.Columns.Add("Action", typeof(string));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("IsError", typeof(int));
                    dt.Columns.Add("Reason", typeof(string));
                    dt.Columns.Add("Remarks", typeof(string));

                    if (string.Equals(type, "AGENT", StringComparison.OrdinalIgnoreCase))
                    {
                        data.AsEnumerable()
                            //.Skip(1) // skip headers
                            .Where(dr => Convert.ToString(dr[0]).Length > 0)
                            .ToList()
                            .ForEach(dr => dt.Rows.Add(Guid.NewGuid().ToString().Replace("-", ""), WorkRequestId, dr[0], dr[1], dr[2], userId, DateTime.Now, 0, string.Empty, dr[3]));
                    }
                    else
                    {
                        data.AsEnumerable()
                            //.Skip(1) // skip headers
                            .Where(dr => Convert.ToString(dr[0]).Length > 0)
                            .ToList()
                            .ForEach(dr => dt.Rows.Add(Guid.NewGuid().ToString().Replace("-", ""), WorkRequestId, "", dr[0], dr[1], userId, DateTime.Now, 0, string.Empty, dr[2]));
                    }
                    string strFile = tempPath + "/TempFolder/MySQL/" + DateTime.Now.Ticks.ToString() + ".csv";

                    //Create directory if not exist... Make sure directory has required rights..
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);

                    //If file does not exist then create it and right data into it..
                    if (!File.Exists(strFile))
                    {
                        FileStream fs = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                        fs.Close();
                        fs.Dispose();
                    }

                    //Generate csv file from where data read
                    _dataTableUtility.CreateCSVfile(dt, strFile);

                    using (MySqlConnection cn1 = new MySqlConnection(sqlConnectionString))
                    {
                        cn1.Open();
                        MySqlBulkLoader bcp1 = new MySqlBulkLoader(cn1);
                        bcp1.TableName = table; //Create ProductOrder table into MYSQL database...
                        bcp1.FieldTerminator = ",";

                        bcp1.LineTerminator = "\r\n";
                        bcp1.FileName = strFile;
                        bcp1.NumberOfLinesToSkip = 0;
                        bcp1.Load();

                        //Once data write into db then delete file..
                        try
                        {
                            File.Delete(strFile);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in ProcessUpdateUsersUploaded : InsertIntoIntermediate - File Delete : " + ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ProcessUpdateUsersUploaded : InsertIntoIntermediate - " + ex);
                throw;
            }
            _logger.LogInformation("ProcessUpdateUsersUploaded : InsertIntoIntermediate - End");
        }
    }
}