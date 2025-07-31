using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForCreateUsersFailed : ISendEmailForCreateUsersFailed
    {
        protected readonly ILogger<SendEmailForCreateUsersFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _fileSettings;

        public SendEmailForCreateUsersFailed(ILogger<SendEmailForCreateUsersFailed> logger, 
            IRepoFactory repoFactory, 
            ISmsUtility smsUtility, 
            IEmailUtility emailUtility, 
            MessageTemplateFactory messageTemplateFactory,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IFileSystem fileSystem,
            IOptions<FilePathSettings> fileSettings
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileSystem = fileSystem;
            _fileSettings = fileSettings.Value;
        }

        public virtual async Task Execute(CreateUsersFailed @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:
            var tenantId = _flexAppContext.TenantId;
            string FileExtension = _fileConfigurationSettings.DefaultExtension;
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.UserProcessedFilePath);


            var model = await repo.GetRepo().FindAll<UsersCreateFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            List<string> files = [model.CustomId + FileExtension];


            if (string.Equals(model.UploadType, UploadTypeEnum.Agent.Value, StringComparison.OrdinalIgnoreCase))
            {
                var messageTemplate = _messageTemplateFactory.BulkCreateAgentFailedEmailTemplate(model.CustomId, tenantId);
                _logger.LogInformation("Bulk upload of agent : Email - " + userEmail);

                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId,files, destPath);
                _logger.LogInformation("Bulk upload of agent  : Send Email - End");

                _logger.LogInformation("Bulk upload of agent  : End");
            }
            else if (string.Equals(model.UploadType, UploadTypeEnum.Agency.Value, StringComparison.OrdinalIgnoreCase))
            {
                var messageTemplate = _messageTemplateFactory.BulkCreateAgencyFailedEmailTemplate(model.CustomId, tenantId);
                _logger.LogInformation("Bulk upload of agency : Email - " + userEmail);

                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId, files, destPath);
                _logger.LogInformation("Bulk upload of agency  : Send Email - End");

                _logger.LogInformation("Bulk upload of agency  : End");
            }
            else if (string.Equals(model.UploadType, UploadTypeEnum.Staff.Value, StringComparison.OrdinalIgnoreCase))
            {
                var messageTemplate = _messageTemplateFactory.BulkCreateStaffFailedEmailTemplate(model.CustomId, tenantId);
                _logger.LogInformation("Bulk upload of Staff : Email - " + userEmail);

                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId, files, destPath);
                _logger.LogInformation("Bulk upload of Staff  : Send Email - End");

                _logger.LogInformation("Bulk upload of Staff  : End");
            }

            await this.Fire<SendEmailForCreateUsersFailed>(EventCondition, serviceBusContext);
        }
    }
}