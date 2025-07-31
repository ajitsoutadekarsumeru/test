using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForMastersImportProcessed : ISendEmailForMastersImportProcessed
    {
        protected readonly ILogger<SendEmailForMastersImportProcessed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IFileSystem _fileSystem;
        public SendEmailForMastersImportProcessed(ILogger<SendEmailForMastersImportProcessed> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, IFileSystem fileSystem)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(MastersImportProcessedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            var MasterFileStatusDto = await _repoFactory.GetRepo().FindAll<MasterFileStatus>()
                                            .Where(a => a.Id == @event.Id)
                                            .SelectTo<MasterFileStatusDtoWithId>()
                                            .FirstOrDefaultAsync();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == MasterFileStatusDto.CreatedBy).FirstOrDefaultAsync();

            if (@event.Status == "processed")
            {
                var messageTemplate = _messageTemplateFactory.MasterImportProcessedEmailTemplate(MasterFileStatusDto, @event.processedrecordscount);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

                await UpdateFileAsync(MasterFileStatusDto.CustomId, "SentSuccessMail");
            }
            else if (@event.Status == "partially")
            {
                string originalFileNameWithoutExt = _fileSystem.Path.GetFileNameWithoutExtension(MasterFileStatusDto.FileName);
                string fileName = originalFileNameWithoutExt + "_Errors.csv";
                string filePath = MasterFileStatusDto.FilePath;

                List<string> files = [fileName];

                var messageTemplate = _messageTemplateFactory.MasterImportEmailPartiallyProcessedTemplate(MasterFileStatusDto, @event.recordsinserted, @event.recordsupdated, @event.nooferrorrecords, @event.totalrecords);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, filePath);

                await UpdateFileAsync(MasterFileStatusDto.CustomId, "SentErrorMail");
            }
            else if (@event.Status == "failed")
            {
                string originalFileNameWithoutExt = _fileSystem.Path.GetFileNameWithoutExtension(MasterFileStatusDto.FileName);
                string fileName = originalFileNameWithoutExt + "_Errors.csv";
                string filePath = MasterFileStatusDto.FilePath;

                List<string> files = [fileName];

                var messageTemplate = _messageTemplateFactory.MasterImporErrorEmailTemplate(MasterFileStatusDto, @event.recordsinserted, @event.recordsupdated, @event.nooferrorrecords, @event.totalrecords);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, filePath);

                await UpdateFileAsync(MasterFileStatusDto.CustomId, "SentErrorMail");
            }
            else if (@event.Status == "norecords")
            {
                string originalFileNameWithoutExt = _fileSystem.Path.GetFileNameWithoutExtension(MasterFileStatusDto.FileName);
                string fileName = originalFileNameWithoutExt + "_Errors.csv";
                string filePath = MasterFileStatusDto.FilePath;

                List<string> files = [fileName];

                var messageTemplate = _messageTemplateFactory.MasterImporErrorEmailTemplate(MasterFileStatusDto, @event.recordsinserted, @event.recordsupdated, @event.nooferrorrecords, @event.totalrecords);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId, files, filePath);

                await UpdateFileAsync(MasterFileStatusDto.CustomId, "SentErrorMail");
            }
        }

        public async Task UpdateFileAsync(string customId, string status)
        {
            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - Start");

            MasterFileStatus entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Description = entity.Description + " -> " + status;
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - File Status Saved Successfully");

            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - End");
        }
    }
}