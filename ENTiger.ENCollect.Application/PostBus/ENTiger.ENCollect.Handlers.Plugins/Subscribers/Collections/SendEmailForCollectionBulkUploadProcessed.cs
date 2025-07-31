using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.FeedbackModule;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendEmailForCollectionBulkUploadProcessed : ISendEmailForCollectionBulkUploadProcessed
    {
        protected readonly ILogger<SendEmailForCollectionBulkUploadProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly IFileValidationUtility _fileValidationUtility;

        public SendEmailForCollectionBulkUploadProcessed(
            ILogger<SendEmailForCollectionBulkUploadProcessed> logger, 
            IRepoFactory repoFactory, 
            IEmailUtility emailUtility, 
            MessageTemplateFactory messageTemplateFactory,
            IFileValidationUtility fileValidationUtility, 
            IOptions<FilePathSettings> fileSettings, 
            IFileSystem fileSystem, 
            IOptions<FileConfigurationSettings> fileConfigurationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;            
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _fileValidationUtility = fileValidationUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
        }

        public virtual async Task Execute(CollectionBulkUploadProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath, _fileSettings.BulkCollectionProcessedFilePath);
            string FileExtension = _fileConfigurationSettings.DefaultExtension;
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            var TenantId = _flexAppContext.TenantId;

            
            _logger.LogInformation("SendEmailForCollectionBulkUploadProcessed : Start");

            var bulkUploaddto = await _repoFactory.GetRepo()
                                    .FindAll<CollectionUploadFile>()
                                    .Where(a => a.Id == @event.Id)
                                    .SelectTo<CollectionUploadFileDtoWithId>()
                                    .FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.CollectionBulklUploadSuccessEmailTemplate(bulkUploaddto);

            string FileName = bulkUploaddto.CustomId + FileExtension;
            List<string> files = [FileName];
            _logger.LogInformation("SendEmailForCollectionBulkUploadProcessed : FilePath - " + filePath + " | File - " + FileName);
            if (_fileValidationUtility.CheckIfFileExists(filePath, FileName))
            {
                string file = filePath + FileName;
                var userEmail = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(x => x.Id == bulkUploaddto.CreatedBy)
                                        .Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

                //Send Email with attachment
                await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, filePath);
            }
            else
            {
                _logger.LogWarning("SendEmailForCollectionBulkUploadProcessed : File - " + FileName + " not found in path : " + filePath);
            }
            _logger.LogInformation("SendEmailForCollectionBulkUploadProcessed : End");


            await this.Fire<SendEmailForCollectionBulkUploadProcessed>(EventCondition, serviceBusContext);
        }
    }
}
