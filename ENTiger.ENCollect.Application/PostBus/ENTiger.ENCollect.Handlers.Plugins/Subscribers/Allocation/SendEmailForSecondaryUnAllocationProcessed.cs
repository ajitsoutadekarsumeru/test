using System.Data;
using System.IO.Abstractions;
using System.IO.Compression;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using System.IO.Compression;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForSecondaryUnAllocationProcessed : ISendEmailForSecondaryUnAllocationProcessed
    {
        protected readonly ILogger<SendEmailForSecondaryUnAllocationProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly DatabaseSettings _databaseSettings;

        private string hostName = string.Empty;
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        private GetDataRequestDto requestGetData = new GetDataRequestDto();
        private string _unAllocationType;

        public SendEmailForSecondaryUnAllocationProcessed(ILogger<SendEmailForSecondaryUnAllocationProcessed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, ICsvExcelUtility csvExcelUtility, IOptions<FilePathSettings> fileSettings, IFlexHost flexHost, IOptions<DatabaseSettings> databaseSettings, IFileSystem fileSystem)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _flexHost = flexHost;
            _databaseSettings = databaseSettings.Value;
        }

        public virtual async Task Execute(SecondaryUnAllocationProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.UnAllocationProcessedFilePath);
            _flexAppContext = @event.AppContext; //do not remove this line
            string tenantId = _flexAppContext.TenantId;
            var repo = _repoFactory.Init(@event);
            _unAllocationType = @event.UnAllocationType;
            //TODO: Write your business logic here:
            hostName = _flexAppContext.HostName;
            var dbType = _databaseSettings.DBType;

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbUtilityEnum);

            //Step 5: Get Secondary UnAllocation Details
            //construct the request for getting data
            requestGetData.SpName = "SecondaryUnAllocation_GetData";
            requestGetData.TenantId = tenantId;
            requestGetData.WorkRequestId = @event.CustomId;
            var requestGetDatavalues = new Dictionary<string, string>();
            requestGetDatavalues.Add("WorkRequestId", @event.CustomId);
            requestGetData.Parameters = requestGetDatavalues;
            //Get Data
            DataTable AttachmentData = await dbUtility.GetData(requestGetData);

            //Step 6: Clean UnAllocation Records
            //construct the request for executing the SP
            request.SpName = "SecondaryUnAllocation_CleanUp";
            request.TenantId = tenantId;
            request.WorkRequestId = @event.CustomId;
            var values = new Dictionary<string, string>();
            values.Add("WorkRequestId", @event.CustomId);
            request.Parameters = values;
            //Call the SP
            await dbUtility.ExecuteSP(request);

            string fileName = @event.CustomId;
            bool folderExists = Directory.Exists(destPath);

            if (!folderExists)
                Directory.CreateDirectory(destPath);

            string destFilePath = Path.Combine(destPath, fileName + ".csv");
            if (File.Exists(destFilePath))
            {
                File.Delete(destFilePath);
            }

            File.WriteAllText(destFilePath, _csvExcelUtility.ConvertDataTableToString(AttachmentData));

            string zipFilePath = Path.Combine(destPath, fileName + ".zip");
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }
            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                zip.CreateEntryFromFile(destFilePath, fileName + ".csv");
            }

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.UnAllocationSecondarySuccessTemplate(@event.CustomId, @event.FileName, @event.AppContext.TenantId);
            _logger.LogInformation("SecondaryUnAllocationSuccessService : Email - " + @event.EMail);

            //send with Attachment
            List<string> files = [fileName + ".zip"];
            await _emailUtility.SendEmailAsync(@event.EMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);
            _logger.LogInformation("SecondaryUnAllocationSuccessService : Send Email - End");
        }
    }
}