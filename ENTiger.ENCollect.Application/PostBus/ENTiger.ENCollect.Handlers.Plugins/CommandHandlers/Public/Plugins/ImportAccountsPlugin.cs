using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportAccountsPlugin : FlexiPluginBase, IFlexiPlugin<ImportAccountsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13c90aff307923d4b43a4829d1036c";
        public override string FriendlyName { get; set; } = "ImportAccountsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ImportAccountsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected MasterFileStatus? _model;
        protected FlexAppContextBridge? _flexAppContext;
        string spName = "usp_ImportAccounts_";

        string userEmail = string.Empty;
        string hostName = string.Empty;

        private readonly ICsvExcelUtility _csvExcelUtility;

        private readonly DatabaseSettings _databaseSettings;

        private readonly FilePathSettings _fileSettings;
        ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        GetDataRequestDto requestGetData = new GetDataRequestDto();
        InsertIntermediateTableRequestDto insertRequest = new InsertIntermediateTableRequestDto();

        private readonly IFileSystem _fileSystem;

        public ImportAccountsPlugin(ILogger<ImportAccountsPlugin> logger,
            IFlexHost flexHost,
            IRepoFactory repoFactory,
            ICsvExcelUtility csvExcelUtility,
            IOptions<DatabaseSettings> databaseSettings,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _csvExcelUtility = csvExcelUtility;
            _databaseSettings = databaseSettings.Value;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(ImportAccountsPostBusDataPacket packet)
        {
            string dbType = _databaseSettings.DBType;
            string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            hostName = _flexAppContext.HostName;

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbUtilityEnum);

            var user = _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _flexAppContext.UserId).FirstOrDefault();

            userEmail = user?.PrimaryEMail;

            _model = _flexHost.GetDomainModel<MasterFileStatus>().ImportAccounts(packet.Cmd);
            _model.FilePath = _filepath;
            _repoFactory.GetRepo().InsertOrUpdate(_model);

            int records1 = await _repoFactory.GetRepo().SaveAsync();
            if (records1 > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(MasterFileStatus).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(MasterFileStatus).Name, _model.Id);
            }


            var values = new Dictionary<string, string>();
            values.Add("WorkRequestId", _model.CustomId);

            _logger.LogDebug("ImportAccountsByAPIFFPlugin :  Truncate Intermediate Table ");
            //construct the request for executing the SP
            request.SpName = spName + "Truncate";
            request.TenantId = _flexAppContext.TenantId;
            request.Parameters = values;
            //Call the SP
            var TruncateIntermediateTable = await dbUtility.ExecuteSP(request);

            if (TruncateIntermediateTable)
            {
                await UpdateFile(_model.CustomId, "TruncatedIntermediateTable");

                _logger.LogDebug("ImportAccountsByAPIFFPlugin :  Excel to DataTable ");
                var dt = _csvExcelUtility.ToDataTable(packet.Cmd.Dto.items, _model.CustomId);

                _logger.LogDebug("ImportAccountsByAPIFFPlugin :  DataTable Count - " + dt.Rows.Count);
                await UpdateFile(_model.CustomId, "PayloadToDataTable | Count - " + dt.Rows.Count);

                int imported = 0;
                string csvFile = Path.Combine(_filepath, Path.GetFileNameWithoutExtension(_model.CustomId) + ".csv");

                _logger.LogDebug("ImportAccountsByAPIFFPlugin :  Generate CSV File - " + csvFile);
                _csvExcelUtility.ToCSV(dt, csvFile);
                await UpdateFile(_model.CustomId, "ToCSV");

                _logger.LogDebug("ImportAccountsByAPIFFPlugin :  " + dbType + " - Bulk Insert Records to Intermediate Table");
                //fill the insert request
                insertRequest.TableName = "ImportAccounts_Intermediate";
                insertRequest.FileName = csvFile;
                insertRequest.Table = dt;
                insertRequest.TenantId = _flexAppContext.TenantId;
                //call the InsertRecordsIntoIntermediateTable
                imported = await dbUtility.InsertRecordsIntoIntermediateTable(insertRequest);

                await UpdateFile(_model.CustomId, "ImportedToIntermediateTable - " + imported);


                if (imported > 0)
                {
                    _logger.LogDebug("ImportAccountsByAPIFFPlugin : Run Validations");
                    //Call the SP
                    request.SpName = spName + "Validate";
                    var Validations = await dbUtility.ExecuteSP(request);
                    await UpdateFile(_model.CustomId, "Validated - " + Validations);

                    _logger.LogDebug("ImportAccountsByAPIFFPlugin : Set Insert Flag In IntermediateTable");
                    //Call the SP
                    request.SpName = spName + "SetFlag";
                    var SetInsertFlag = await dbUtility.ExecuteSP(request);
                    await UpdateFile(_model.CustomId, "FlagSet - " + SetInsertFlag);

                    _logger.LogDebug("ImportAccountsByAPIFFPlugin : Update Records from Intermediate to Main table");
                    //Call the SP
                    request.SpName = spName + "Update";
                    var update = await dbUtility.ExecuteSP(request);
                    await UpdateFile(_model.CustomId, "Updated - " + update);

                    _logger.LogDebug("ImportAccountsByAPIFFPlugin : Insert New Records from Intermediate to Main table");
                    //Call the SP
                    request.SpName = spName + "Insert";
                    var insert = await dbUtility.ExecuteSP(request);
                    await UpdateFile(_model.CustomId, "Inserted - " + insert);

                    _logger.LogDebug("ImportAccountsByAPIFFPlugin : Get Records");
                    //construct the request for getting data
                    requestGetData.SpName = spName + "GetData";
                    requestGetData.TenantId = _flexAppContext.TenantId;
                    requestGetData.Parameters = values;
                    //Get Data
                    var records = await dbUtility.GetData(requestGetData);
                    if (records != null)
                    {
                        _logger.LogDebug("ImportAccountsByAPIFFPlugin : Count - " + records.Rows.Count);
                        var errorRecords = records.Select("IsError = true").ToList();

                        _logger.LogDebug("ImportAccountsByAPIFFPlugin : Error Count - " + errorRecords.Count);
                        if (errorRecords.Count == 0)
                        {
                            await UpdateFileStatus(_model.CustomId, "Processed");
                            _logger.LogDebug("ImportAccountsByAPIFFPlugin : Send Successfully Processed Mail");

                            SendMail(_model, "Success");
                            await UpdateFile(_model.CustomId, "SentSuccessMail");
                        }
                        else
                        {
                            await UpdateFileStatus(_model.CustomId, "Partially Processed");
                            _logger.LogDebug("ImportAccountsByAPIFFPlugin : Send Partially Processed Error Mail");
                            string fileName = Path.GetFileNameWithoutExtension(_model.CustomId) + "_Errors.csv";
                            string errorFile = Path.Combine(_filepath, fileName);
                            //generate CSV
                            DataTable dt1 = records.Select("IsError = true").CopyToDataTable();
                            _csvExcelUtility.ToCSV(dt1, errorFile);

                            // SendMail(model, "Partial", records, fileName);
                            await UpdateFile(_model.CustomId, "SentErrorMail");
                        }
                        _logger.LogInformation("ImportAccountsByAPIFFPlugin : Archive Error Records into Intermediate error table");
                        //Call the SP
                        request.SpName = spName + "Archive";
                        var MoveErrorRecords = await dbUtility.ExecuteSP(request);
                        _logger.LogInformation("ImportAccountsByAPIFFPlugin : Error Records moved to Intermediate error table and table cleaned");
                    }
                }
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task UpdateFile(string customId, string status)
        {
            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFile - Start");

            MasterFileStatus entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Description = (entity.Description + " -> " + status).Length <= 2000 ? entity.Description + " -> " + status : entity.Description;
            entity.UpdateImportAccounts(entity);
            entity.SetAddedOrModified();
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFile - Saved Successfully");

            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFile - End");
        }

        private async Task UpdateFileStatus(string customId, string status)
        {
            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFileStatus - Start");

            MasterFileStatus? entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Status = status;
            entity.Description = (entity.Description + " -> " + status).Length <= 2000 ? entity.Description + " -> " + status : entity.Description;
            entity.UpdateImportAccounts(entity);
            entity.SetAddedOrModified();
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFileStatus - File Status Saved Successfully");

            _logger.LogDebug("ImportAccountsByAPIFFPlugin : UpdateFileStatus - End");
        }

        private void SendMail(MasterFileStatus model, string type, DataTable dt = null, string file = null)
        {
            type = "ImportAccountsAPI" + type;
            _logger.LogInformation("ImportAccountsByAPIFFPlugin : " + type + " - Start");

            IMessageTemplate? messageTemplate = null;

            // _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

            _logger.LogInformation("ImportAccountsByAPIFFPlugin : " + type + " - End");
        }
    }
}