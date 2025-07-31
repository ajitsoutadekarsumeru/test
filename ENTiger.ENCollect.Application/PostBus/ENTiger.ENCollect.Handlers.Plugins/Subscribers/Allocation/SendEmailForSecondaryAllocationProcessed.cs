using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForSecondaryAllocationProcessed : ISendEmailForSecondaryAllocationProcessed
    {
        protected readonly ILogger<SendEmailForSecondaryAllocationProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly SSISPackageSettings _ssisPackageSettings;
        private readonly DatabaseSettings _databaseSettings;
        public SendEmailForSecondaryAllocationProcessed(
            ILogger<SendEmailForSecondaryAllocationProcessed> logger,
            IRepoFactory repoFactory,
            ISmsUtility smsUtility,
            IEmailUtility emailUtility,
            MessageTemplateFactory messageTemplateFactory,
            IOptions<FilePathSettings> fileSettings,
            IFileSystem fileSystem,
            IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IOptions<SSISPackageSettings> ssisPackageSettings,
            IOptions<DatabaseSettings> databaseSettings
            )
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _ssisPackageSettings = ssisPackageSettings.Value;
            _databaseSettings = databaseSettings.Value;
        }

        public virtual async Task Execute(SecondaryAllocationProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForSecondaryAllocationProcessed : Start");
            string FileExtension = _databaseSettings.DBType.ToLower() == DBTypeEnum.MsSQL.Value ? _ssisPackageSettings.PackageSettings.FileExtension : _fileConfigurationSettings.DefaultExtension;
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.AllocationProcessedFilePath);
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            var tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<SecondaryAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            IMessageTemplate? messageTemplate = null;
            List<string> files = [model.CustomId + FileExtension];

            if (string.Equals(@event.FileType, "agent", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationAgentProcessedTemplate(model.CustomId, model.FileName, @event.AppContext.TenantId);
            }
            else if (string.Equals(@event.FileType, "staff", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationStaffProcessedTemplate(model.CustomId, model.FileName, @event.AppContext.TenantId);
            }
            else if (string.Equals(@event.FileType, "telecaller", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.SecondaryAllocationTeleCallerProcessedTemplate(model.CustomId, model.FileName, @event.AppContext.TenantId);
            }

            _logger.LogDebug("SendEmailForSecondaryAllocationProcessed : Send Email : " + userEmail);

            //Send Email with attachment
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

            _logger.LogDebug("SendEmailForSecondaryAllocationProcessed : Email Sent Successfully");
        }
    }
}