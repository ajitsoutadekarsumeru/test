using System.Data;
using System.IO.Abstractions;
using System.IO.Compression;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForUpdateUsersProcessed : ISendEmailForUpdateUsersProcessed
    {
        protected readonly ILogger<SendEmailForUpdateUsersProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexHost _flexHost;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
       
        private string hostName = string.Empty;
        private ExecuteSpRequestDto request = new ExecuteSpRequestDto();
        private GetDataRequestDto requestGetData = new GetDataRequestDto();
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly DatabaseSettings _databaseSettings;

        public SendEmailForUpdateUsersProcessed(ILogger<SendEmailForUpdateUsersProcessed> logger, 
            IRepoFactory repoFactory, 
            ISmsUtility smsUtility, 
            IEmailUtility emailUtility, 
            MessageTemplateFactory messageTemplateFactory,
            ICsvExcelUtility csvExcelUtility, 
            IOptions<FilePathSettings> fileSettings,
            IOptions<DatabaseSettings> databaseSettings,
            IFileSystem fileSystem, 
            IFlexHost flexHost)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _databaseSettings = databaseSettings.Value;
            _flexHost = flexHost;
        }

        public virtual async Task Execute(UpdateUsersProcessed @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForUpdateUsersProcessed : Start");
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.UserProcessedFilePath);
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            hostName = _flexAppContext.HostName;
            var dbType = _databaseSettings.DBType;

            //fetch the factory instance for db utility
            var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

            //fetch the correct enum w.r.t the paymentPartner
            var dbUtilityEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
            var dbUtility = utility.GetUtility(dbUtilityEnum);

            string tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<UsersUpdateFile>()
                                .Where(u => u.Id == @event.Id).SelectTo<UsersUpdateFileDtoWithId>()
                                .FirstOrDefaultAsync();

            var PrimaryEMail = await repo.GetRepo().FindAll<ApplicationUser>()
                                .Where(a => a.Id == model.CreatedBy).Select(b => b.PrimaryEMail)
                                .FirstOrDefaultAsync();

            var values = new Dictionary<string, string>();
            //Step 5: Get Users Update Details
            values = new Dictionary<string, string>();
            values.Add("WorkRequestId", model.CustomId);
            values.Add("Type", model.UploadType);

            //construct the request for getting data
            requestGetData.SpName = "UsersUpdate_GetData";
            requestGetData.TenantId = tenantId;
            requestGetData.Parameters = values;
            //Get Data
            DataTable AttachmentData = await dbUtility.GetData(requestGetData);

            //Step 6: Clean UsersUpdate Records
            values = new Dictionary<string, string>();
            values.Add("WorkRequestId", model.CustomId);

            //construct the request for executing the SP
            request.SpName = "UsersUpdate_CleanUp";
            request.TenantId = tenantId;
            request.Parameters = values;
            //Call the SP
            await dbUtility.ExecuteSP(request);

            string fileName = model.CustomId;
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
            var messageTemplate = _messageTemplateFactory.UploadUsersSuccessTemplate(model.CustomId, model.FileName, @event.AppContext.HostName);

            List<string> files = [fileName + ".zip"];
            _logger.LogInformation("SendEmailForUpdateUsersProcessed : SendMail : Email - " + PrimaryEMail);
            await _emailUtility.SendEmailAsync(PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId, files, destPath);
            _logger.LogInformation("SendEmailForUpdateUsersProcessed : SendMail : Sent - " + PrimaryEMail);

            _logger.LogInformation("SendEmailForUpdateUsersProcessed : End");
        }
    }
}