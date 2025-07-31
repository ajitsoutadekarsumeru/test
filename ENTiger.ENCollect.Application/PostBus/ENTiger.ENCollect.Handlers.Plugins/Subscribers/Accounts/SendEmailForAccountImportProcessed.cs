using System.Data;
using System.IO.Abstractions;
using System.Reflection;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendEmailForAccountImportProcessed : ISendEmailForAccountImportProcessed
    {
        protected readonly ILogger<SendEmailForAccountImportProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        string spName = "usp_ImportAccounts_";
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly DatabaseSettings _databaseSettings;
        private readonly FilePathSettings _fileSettings;
        string dbType = string.Empty;
        string hostName = string.Empty;
        protected readonly IFlexHost _flexHost;


        private readonly IFileSystem _fileSystem;
        public SendEmailForAccountImportProcessed(
            ILogger<SendEmailForAccountImportProcessed> logger,
            IFlexHost flexHost,
            IOptions<DatabaseSettings> databaseSettings,
            IRepoFactory repoFactory, ISmsUtility smsUtility,
            IEmailUtility emailUtility,
            MessageTemplateFactory messageTemplateFactory,
            ICsvExcelUtility csvExcelUtility,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
            _flexHost = flexHost;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(AccountImportProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForAccountImportProcessed : Start | JSON - " + JsonConvert.SerializeObject(@event));
            string _Tempfilepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.TemporaryPath);
            _flexAppContext = @event.AppContext; //do not remove this line
            string tenantId = _flexAppContext.TenantId;
            var repo = _repoFactory.Init(@event);
            hostName = _flexAppContext.HostName;
            dbType = _databaseSettings.DBType;

            MasterFileStatus? _model = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == _model.CreatedBy).FirstOrDefaultAsync();

            var values = new Dictionary<string, string>();
            values.Add("WorkRequestId", _model.CustomId);

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbUtilityEnum);

            _logger.LogDebug("SendEmailForAccountImportProcessed : Get Records");
            //Get Data
            GetDataRequestDto requestData = new GetDataRequestDto();
            requestData.WorkRequestId = _model.CustomId;
            requestData.SpName = spName + "GetData";
            requestData.TenantId = tenantId;
            requestData.Parameters = values;
            var records = await dbUtility.GetData(requestData);
            if (records != null)
            {
                _logger.LogDebug("SendEmailForAccountImportProcessed : Count - " + records.Rows.Count);
                var errorRecords = records.Select("IsError = true").ToList();
                int totalRecords = records.Rows.Count;
                int errorCount = errorRecords.Count;
                int successCount = totalRecords - errorCount;

                _logger.LogDebug("SendEmailForAccountImportProcessed : Total Records - {Total}, Errors - {Errors}, Success - {Success}",
                    totalRecords, errorCount, successCount);

                if (errorCount == 0)
                {
                    await UpdateFileStatus(_model, FileStatusEnum.Processed.Value);
                    var messageTemplate = _messageTemplateFactory.AccountImportSuccessTemplate(_model.FileName, tenantId);
                    await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
                    await UpdateFile(_model, "SentSuccessMail");
                }
                else if (successCount > 0)
                {
                    await UpdateFileStatus(_model, FileStatusEnum.Partial.Value);

                    string errorFileName = $"{_model.CustomId}_Errors.csv";
                    string errorFilePath = Path.Combine(_Tempfilepath, errorFileName);
                    _csvExcelUtility.ToCSV(errorRecords.CopyToDataTable(), errorFilePath);

                    var messageTemplate = _messageTemplateFactory.AccountImportPartialTemplate(_model.CustomId, records, tenantId);
                    var attachments = new List<string> { _model.FileName };

                    await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId, attachments, _model.FilePath);
                    await UpdateFile(_model, "SentPartialErrorMail");
                }

                _logger.LogInformation("SendEmailForAccountImportPartial : Archive Error Records into Intermediate error table");

                ExecuteSpRequestDto request = new ExecuteSpRequestDto();
                request.SpName = spName + "Archive";
                request.TenantId = tenantId;
                request.Parameters = values;
                //Call the SP
                var MoveErrorRecords = await dbUtility.ExecuteSP(request);
                _logger.LogInformation("SendEmailForAccountImportPartial : Error Records moved to Intermediate error table and table cleaned");
            }
            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire<SendEmailForAccountImportProcessed>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }

        private async Task UpdateFile(MasterFileStatus model, string status)
        {
            _logger.LogDebug("SendEmailForAccountImportProcessed : UpdateFile - Start");
            model.Description = model.Description + " -> " + status;
            model.SetAddedOrModified();

            _repoFactory.GetRepo().InsertOrUpdate(model);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("SendEmailForAccountImportProcessed : UpdateFile - End");
        }

        private async Task UpdateFileStatus(MasterFileStatus model, string status)
        {
            _logger.LogDebug("SendEmailForAccountImportProcessed : UpdateFileStatus - Start");

            model.Status = status;
            model.Description = model.Description + " -> " + status;
            model.FileProcessedDateTime = DateTime.Now;
            model.SetAddedOrModified();

            _repoFactory.GetRepo().InsertOrUpdate(model);
            await _repoFactory.GetRepo().SaveAsync();

            _logger.LogDebug("SendEmailForAccountImportProcessed : UpdateFileStatus - End");
        }
    }
}