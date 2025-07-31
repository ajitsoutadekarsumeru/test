using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForPrimaryAllocationProcessed : ISendEmailForPrimaryAllocationProcessed
    {
        protected readonly ILogger<SendEmailForPrimaryAllocationProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly SSISPackageSettings _ssisPackageSettings;
        private readonly DatabaseSettings _databaseSettings;
        public SendEmailForPrimaryAllocationProcessed(ILogger<SendEmailForPrimaryAllocationProcessed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory,
            IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem, IOptions<FileConfigurationSettings> fileConfigurationSettings
            , IOptions<SSISPackageSettings> ssisPackageSettings, IOptions<DatabaseSettings> databaseSettings)
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

        public virtual async Task Execute(PrimaryAllocationProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForPrimaryAllocationProcessed : Start");
            string FileExtension = _databaseSettings.DBType.ToLower() == DBTypeEnum.MsSQL.Value ? _ssisPackageSettings.PackageSettings.FileExtension : _fileConfigurationSettings.DefaultExtension;
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.AllocationProcessedFilePath);
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            var tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<PrimaryAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            IMessageTemplate? messageTemplate = null;
            List<string> files = [model.CustomId + FileExtension];

            if (string.Equals(@event.FileType, "agency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationAgencyProcessedTemplate(model.CustomId, model.FileName, tenantId);
            }
            else if (string.Equals(@event.FileType, "telecallingagency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationTCAgencyProcessedTemplate(model.CustomId, model.FileName, tenantId);
            }
            else if (string.Equals(@event.FileType, "allocationowner", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationOwnerProcessedTemplate(model.CustomId, model.FileName, tenantId);
            }


            _logger.LogInformation("SendEmailForPrimaryAllocationProcessed : Send Email : " + userEmail);

            //Send email with attachment
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, destPath);

            _logger.LogInformation("SendEmailForPrimaryAllocationProcessed - : End");
        }
    }
}