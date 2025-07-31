using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;
using Sumeru.Flex;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForCreateUsersProcessed : ISendEmailForCreateUsersProcessed
    {
        protected readonly ILogger<SendEmailForCreateUsersProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public SendEmailForCreateUsersProcessed(ILogger<SendEmailForCreateUsersProcessed> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, 
            IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem, IOptions<FileConfigurationSettings> fileConfigurationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
        }

        public virtual async Task Execute(CreateUsersProcessed @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForCreateUsersProcessed : Start");
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.UserProcessedFilePath);
            string FileExtension = _fileConfigurationSettings.DefaultExtension;
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            var tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<UsersCreateFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            IMessageTemplate? messageTemplate = null;
            List<string> files = [model.CustomId + FileExtension];

            //TODO: Write your business logic here:

            UsersCreateFileDtoWithId dto = new UsersCreateFileDtoWithId();
            dto.CustomId = model.CustomId;
            dto.FileName = model.FileName;
            dto.UploadType = model.UploadType;

            if (string.Equals(dto.UploadType, UploadTypeEnum.Agent.Value, StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.BulkCreateAgentSuccessEmailTemplate(dto);
                _logger.LogInformation("SendEmailForCreateAgentProcessed : Send Email : " + userEmail);
                //Send email with attachment
                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

                _logger.LogInformation("SendEmailForCreateAgentProcessed - : End");
            }
            else if (string.Equals(dto.UploadType, UploadTypeEnum.Agency.Value, StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.BulkCreateAgencySuccessEmailTemplate(dto);
                _logger.LogInformation("SendEmailForCreateAgencyProcessed : Send Email : " + userEmail);
                //Send email with attachment
                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

                _logger.LogInformation("SendEmailForCreateAgencyProcessed - : End");
            }
            else if (string.Equals(dto.UploadType, UploadTypeEnum.Staff.Value, StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.BulkCreateStaffSuccessEmailTemplate(dto);
                _logger.LogInformation("SendEmailForCreateStaffProcessed : Send Email : " + userEmail);
                //Send email with attachment
                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

                _logger.LogInformation("SendEmailForCreateStaffProcessed - : End");
            }

            await this.Fire<SendEmailForCreateUsersProcessed>(EventCondition, serviceBusContext);
        }
    }
}