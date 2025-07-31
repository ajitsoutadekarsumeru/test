using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendEmailForMastersImportFailed : ISendEmailForMastersImportFailed
    {
        protected readonly ILogger<SendEmailForMastersImportFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForMastersImportFailed(ILogger<SendEmailForMastersImportFailed> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(MastersImportFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            var MasterFileStatusDto = await _repoFactory.GetRepo().FindAll<MasterFileStatus>()
                                        .Where(a => a.Id == @event.Id)
                                        .SelectTo<MasterFileStatusDtoWithId>()
                                        .FirstOrDefaultAsync();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                .Where(a => a.Id == MasterFileStatusDto.CreatedBy)
                                .FirstOrDefaultAsync();

            if (@event.ErrorReason == "incorrectheaders")
            {
                var messageTemplate = _messageTemplateFactory.MasterImportHeaderMismatchEmailTemplate(MasterFileStatusDto);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

                await UpdateFileAsync(MasterFileStatusDto.CustomId, "SentErrorMail");
            }
            else if (@event.ErrorReason == "failed")
            {
                var messageTemplate = _messageTemplateFactory.MasterImportFailedEmailTemplate(MasterFileStatusDto);

                _logger.LogInformation("SendEMailOnMasterImport : Email - " + user.PrimaryEMail);
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            }

            await Task.CompletedTask;
        }

        public async Task UpdateFileAsync(string customId, string status)
        {
            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - Start");

            MasterFileStatus? entity = await _repoFactory.GetRepo().FindAll<MasterFileStatus>().Where(x => x.CustomId == customId).FirstOrDefaultAsync();
            entity.Description = entity.Description + " -> " + status;
            _repoFactory.GetRepo().InsertOrUpdate(entity);
            await _repoFactory.GetRepo().SaveAsync();
            _logger.LogDebug("MasterImportFFPlugin : UpdateFileStatus - File Status Saved Successfully");

            _logger.LogDebug("MasterImportFFPlugin : UpdateFile - End");
        }
    }
}