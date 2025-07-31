using ENTiger.ENCollect.DomainModels;
using System.IO.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class SendEmailForBulkTrailProcessed : ISendEmailForBulkTrailProcessed
    {
        protected readonly ILogger<SendEmailForBulkTrailProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        string TenantId = string.Empty;

        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly IFileValidationUtility _fileValidationUtility;
        private readonly SSISPackageSettings _ssisPackageSettings;
        private readonly DatabaseSettings _databaseSettings;
        public SendEmailForBulkTrailProcessed(ILogger<SendEmailForBulkTrailProcessed> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory,
            IFileValidationUtility fileValidationUtility, IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem, IOptions<FileConfigurationSettings> fileConfigurationSettings,
            IOptions<SSISPackageSettings> ssisPackageSettings,
            IOptions<DatabaseSettings> databaseSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _fileValidationUtility = fileValidationUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _ssisPackageSettings = ssisPackageSettings.Value;
            _databaseSettings = databaseSettings.Value;
        }

        public virtual async Task Execute(BulkTrailProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.BulkTrailProcessedFilePath);
            string FileExtension = _databaseSettings.DBType.ToLower() == DBTypeEnum.MsSQL.Value ? _ssisPackageSettings.PackageSettings.FileExtension : _fileConfigurationSettings.DefaultExtension;
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            TenantId = _flexAppContext.TenantId;
            //TODO: Write your business logic here:
            _logger.LogInformation("BulkTrailSucceededService : Start");

            var bulkTraildto = await _repoFactory.GetRepo().FindAll<BulkTrailUploadFile>()
                                        .Where(a => a.Id == @event.Id)
                                        .SelectTo<BulkTrailUploadFileDtoWithId>().FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.BulkTrailUploadSucceededEmailTemplate(bulkTraildto);

            string FileName = bulkTraildto.CustomId + FileExtension;
            List<string> files = [FileName];
            _logger.LogInformation("BulkTrailSucceededService : FilePath - " + filePath + " | File - " + FileName);
            if (_fileValidationUtility.CheckIfFileExists(filePath, FileName))
            {
                string file = filePath + FileName;
                var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                            .Where(x => x.Id == bulkTraildto.CreatedBy)
                                            .Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

                //Send Email with attachment
                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, filePath);
            }
            else
            {
                _logger.LogWarning("BulkTrailSucceededService : File - " + FileName + " not found in path : " + filePath);
            }
            _logger.LogInformation("BulkTrailSucceededService : End");
        }
    }
}