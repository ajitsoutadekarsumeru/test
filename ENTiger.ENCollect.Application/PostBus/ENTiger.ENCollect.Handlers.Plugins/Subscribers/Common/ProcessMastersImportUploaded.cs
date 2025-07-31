using ENTiger.ENCollect.DomainModels.Utilities;
using ENTiger.ENCollect.DomainModels.Utilities.File_Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessMastersImportUploaded : IProcessMastersImportUploaded
    {
        protected readonly ILogger<ProcessMastersImportUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private List<string> staticHeaders = new List<string>();
        protected MasterFileStatus? inputmodel;
        protected string ErrorReason;
        protected string filestatus;
        protected int processedrecordscount;
        protected int recordsinserted;
        protected int recordsupdated;
        protected int nooferrorrecords;
        protected int totalrecords;
        private string hostName = string.Empty;

        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        private GetDataRequestDto requestGetData = new GetDataRequestDto();
        private InsertIntermediateTableRequestDto insertRequest = new InsertIntermediateTableRequestDto();

        private IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
        private readonly ICsvExcelUtility _csvExcelUtility;

        private readonly DatabaseSettings _databaseSettings;
        private readonly CsvExcelSettings _csvSettings;

        public ProcessMastersImportUploaded(ILogger<ProcessMastersImportUploaded> logger, IRepoFactory repoFactory, ICsvExcelUtility csvExcelUtility, IOptions<DatabaseSettings> databaseSettings, IFlexHost flexHost, IOptions<CsvExcelSettings> csvSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _csvExcelUtility = csvExcelUtility;
            _databaseSettings = databaseSettings.Value;
            _csvSettings = csvSettings.Value;
            _flexHost = flexHost;
        }

        public virtual async Task Execute(MastersImportUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            string spName = "usp_";
            int totalRecordsCount = 0;
            _repoFactory.Init(@event);
            hostName = _flexAppContext.HostName;
            var dbType = _databaseSettings.DBType;

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbUtilityEnum);

            inputmodel = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(a => a.Id == @event.Id).FirstOrDefaultAsync();

            _logger.LogDebug("MasterImportFFPlugin :  Start");

            try
            {
                spName = spName + inputmodel.UploadType + "_";
                _logger.LogDebug("MasterImportFFPlugin :  Truncate Intermediate Table ");
                request.SpName = spName + "Truncate";
                request.TenantId = _flexAppContext.TenantId;

                //Call the SP
                var TruncateIntermediateTable = await dbUtility.ExecuteSP(request);

                if (TruncateIntermediateTable)
                {
                    await UpdateFileAsync(inputmodel.CustomId, "TruncatedIntermediateTable");

                    _logger.LogDebug("MasterImportFFPlugin :  Excel to DataTable ");
                    var dt = _csvExcelUtility.ExcelToDataTable(Path.Combine(inputmodel.FilePath, inputmodel.FileName));

                    totalRecordsCount = dt.Rows.Count;
                    _logger.LogDebug("MasterImportFFPlugin :  DataTable Count - " + totalRecordsCount);

                    await UpdateFileAsync(inputmodel.CustomId, "ExcelToDataTable | Count - " + totalRecordsCount);
                    bool IsCorrectFileHeader = await ValidateUsersUpdateHeadersAsync(dt, inputmodel.UploadType);

                    int imported = 0;
                    if (IsCorrectFileHeader)
                    {
                        if (string.Equals(dbType, "mysql", StringComparison.OrdinalIgnoreCase))
                        {
                            string csvFile = Path.Combine(inputmodel.FilePath, Path.GetFileNameWithoutExtension(inputmodel.FileName) + ".csv");

                            _logger.LogDebug("MasterImportFFPlugin :  Generate CSV File - " + csvFile);
                            _csvExcelUtility.ToCSV(dt, csvFile);

                            await UpdateFileAsync(inputmodel.CustomId, "ToCSV");

                            _logger.LogDebug("MasterImportFFPlugin :  " + dbType + " - Bulk Insert Records to Intermediate Table");

                            //fill the insert request
                            insertRequest.TableName = inputmodel.UploadType + "_Intermediate";
                            insertRequest.FileName = csvFile;
                            insertRequest.TenantId = _flexAppContext.TenantId;
                            //TODO: review addition of delimiter - was using default | delimiter and causing failure
                            insertRequest.Delimiter = _csvSettings.delimiter;

                            //call the InsertRecordsIntoIntermediateTable
                            imported = await dbUtility.InsertRecordsIntoIntermediateTable(insertRequest);

                            await UpdateFileAsync(inputmodel.CustomId, "ImportedToIntermediateTable");
                        }
                        else
                        {
                            _logger.LogDebug("MasterImportFFPlugin :  " + dbType + " - Bulk Insert Records to Intermediate Table");
                            //fill the insert request
                            insertRequest.TableName = inputmodel.UploadType + "_Intermediate";
                            insertRequest.Table = dt;
                            insertRequest.TenantId = _flexAppContext.TenantId;
                            //call the InsertRecordsIntoIntermediateTable
                            imported = await dbUtility.InsertRecordsIntoIntermediateTable(insertRequest);

                            await UpdateFileAsync(inputmodel.CustomId, "ImportedToIntermediateTable");
                        }
                    }
                    else
                    {
                        _logger.LogDebug("MasterImportFFPlugin : Send headerMismatched Error Mail");
                        await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Failed.Value);

                        ErrorReason = "incorrectheaders";
                        EventCondition = CONDITION_ONFAILURE;
                    }

                    if (imported > 0)
                    {
                        _logger.LogDebug("MasterImportFFPlugin : Set Insert Flag In IntermediateTable");
                        //Call the SP
                        request.SpName = spName + "SetFlag";
                        var SetInsertFlag = await dbUtility.ExecuteSP(request);
                        await UpdateFileAsync(inputmodel.CustomId, "FlagSet");

                        _logger.LogDebug("MasterImportFFPlugin : Run Validations");
                        //Call the SP
                        request.SpName = spName + "Validate";
                        var Validations = await dbUtility.ExecuteSP(request);
                        await UpdateFileAsync(inputmodel.CustomId, "Validated");

                        _logger.LogDebug("MasterImportFFPlugin : Update Records from Intermediate to Main table");
                        //Call the SP
                        request.SpName = spName + "Update";
                        var update = await dbUtility.ExecuteSP(request);
                        await UpdateFileAsync(inputmodel.CustomId, "Updated");

                        _logger.LogDebug("MasterImportFFPlugin : Insert New Records from Intermediate to Main table");
                        //Call the SP
                        request.SpName = spName + "Insert";
                        var insert = await dbUtility.ExecuteSP(request);
                        await UpdateFileAsync(inputmodel.CustomId, "Inserted");

                        _logger.LogDebug("MasterImportFFPlugin : Get Records");

                        //construct the request for getting data
                        requestGetData.SpName = spName + "GetData";
                        requestGetData.TenantId = _flexAppContext.TenantId;

                        //Get Data
                        var records = await dbUtility.GetData(requestGetData);
                        if (records != null)
                        {
                            _logger.LogDebug("MasterImportFFPlugin : Count - " + records.Rows.Count);
                            var errorRecords = records.Select("IsError = true").ToList();
                            var validRecords = records.Select("IsError = false").ToList();
                            // DataTable dataTable = errorRecords.CopyToDataTable();
                            DataTable dataTable = errorRecords.Any() ? errorRecords.CopyToDataTable() : new DataTable();

                            _logger.LogDebug("MasterImportFFPlugin : Error Count - " + errorRecords.Count);
                            if (errorRecords.Count == 0 && validRecords.Count > 0)
                            {
                                await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Processed.Value);
                                filestatus = "processed";
                                processedrecordscount = records.Rows.Count;
                                _logger.LogDebug("MasterImportFFPlugin : Send Successlly Processed Mail");

                                EventCondition = CONDITION_ONSUCCESS;
                            }
                            else if (errorRecords.Count > 0 && validRecords.Count > 0)
                            {
                                await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Partial.Value);
                                _logger.LogDebug("MasterImportFFPlugin : Send Partially Processed Error Mail");
                                string fileName = Path.GetFileNameWithoutExtension(inputmodel.FileName) + "_Errors.csv";
                                string errorFile = Path.Combine(inputmodel.FilePath, fileName);
                                filestatus = "partially";
                                //generate CSV
                                _csvExcelUtility.ToCSV(dataTable, errorFile);
                                recordsinserted = records.Select("IsError = false AND IsInsert = true").ToList().Count;
                                recordsupdated = records.Select("IsError = false AND IsInsert = false").ToList().Count;
                                nooferrorrecords = records.Select("IsError = true").ToList().Count;
                                totalrecords = records.Select().ToList().Count;

                                EventCondition = CONDITION_ONSUCCESS;
                            }
                            else if (errorRecords.Count > 0)
                            {
                                await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Failed.Value);
                                filestatus = "failed";
                                _logger.LogDebug("MasterImportFFPlugin : Send Failed Error Mail");
                                string fileName = Path.GetFileNameWithoutExtension(inputmodel.FileName) + "_Errors.csv";
                                string errorFile = Path.Combine(inputmodel.FilePath, fileName);
                                //generate CSV
                                _csvExcelUtility.ToCSV(dataTable, errorFile);
                                recordsinserted = records.Select("IsError = false AND IsInsert = true").ToList().Count;
                                recordsupdated = records.Select("IsError = false AND IsInsert = false").ToList().Count;
                                nooferrorrecords = records.Select("IsError = true").ToList().Count;
                                totalrecords = records.Select().ToList().Count;

                                EventCondition = CONDITION_ONSUCCESS;
                            }
                            else
                            {
                                await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Failed.Value);
                                filestatus = "norecords";
                                _logger.LogDebug("MasterImportFFPlugin : Send Failed Error Mail");
                                string fileName = Path.GetFileNameWithoutExtension(inputmodel.FileName) + "_Errors.csv";
                                string errorFile = Path.Combine(inputmodel.FilePath, fileName);
                                //generate CSV
                                _csvExcelUtility.ToCSV(dt, errorFile);
                                recordsinserted = records.Select("IsError = false AND IsInsert = true").ToList().Count;
                                recordsupdated = records.Select("IsError = false AND IsInsert = false").ToList().Count;
                                nooferrorrecords = records.Select("IsError = true").ToList().Count;
                                totalrecords = records.Select().ToList().Count;

                                EventCondition = CONDITION_ONSUCCESS;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await UpdateFileStatus(inputmodel.CustomId, FileStatusEnum.Failed.Value);
                ErrorReason = "failed";
                _logger.LogDebug("Exception in MasterImportFFPlugin :  " + ex);
                EventCondition = CONDITION_ONFAILURE;
            }
            _logger.LogDebug("MasterImportFFPlugin :  End");

            //

            await this.Fire<ProcessMastersImportUploaded>(EventCondition, serviceBusContext);
        }

        public async Task UpdateFileAsync(string customId, string status)
        {
            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - Start");

            MasterFileStatus? entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Description = entity.Description + " -> " + status;
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - File Status Saved Successfully");

            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - End");
        }

        public async Task UpdateFileStatus(string customId, string status)
        {
            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - Start");

            MasterFileStatus? entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Status = status;
            entity.Description = entity.Description + " -> " + status;
            entity.FileProcessedDateTime = DateTime.Now;
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - File Status Saved Successfully");

            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - End");
        }

        public async Task<bool> ValidateUsersUpdateHeadersAsync(DataTable dataTable, string type)
        {
            bool IsCorrectFileHeader = true;

            // Compare headers without changing their case
            var dynamicHeaders = dataTable.Columns.Cast<DataColumn>()
                                                 .Select(x => x.ColumnName?.Trim())
                                                 .ToArray();

            // Use case-insensitive comparison for CategoryMasterId and Code
            var staticHeaders = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                                        .Where(x => string.Equals(x.CategoryMasterId, CategoryMasterEnum.MasterUpdateHeaders.Value) &&
                                                    string.Equals(x.Code, type))
                                        .OrderBy(x => x.Id)
                                        .Select(x => x.Name.Trim())
                                        .ToListAsync();

            // Compare headers, ensuring no differences in content
            var result = staticHeaders.Where(x1 => !dynamicHeaders.Any(x2 => string.Equals(x1, x2)))
                                      .Union(dynamicHeaders.Where(x1 => !staticHeaders.Any(x2 => string.Equals(x1, x2))));

            // If there's a mismatch in headers, return false
            IsCorrectFileHeader = !result.Any();

            return IsCorrectFileHeader;
        }
    }
}