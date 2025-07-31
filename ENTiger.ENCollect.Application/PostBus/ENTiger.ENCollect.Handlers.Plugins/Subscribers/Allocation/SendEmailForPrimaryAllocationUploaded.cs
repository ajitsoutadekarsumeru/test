using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForPrimaryAllocationUploaded : ISendEmailForPrimaryAllocationUploaded
    {
        protected readonly ILogger<SendEmailForPrimaryAllocationUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForPrimaryAllocationUploaded(ILogger<SendEmailForPrimaryAllocationUploaded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PrimaryAllocationUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailForPrimaryAllocationUploaded : Start");
            _flexAppContext = @event.AppContext; //do not remove this line

            var repo = _repoFactory.Init(@event);
            string tenantId = _flexAppContext.TenantId;

            var model = await repo.GetRepo().FindAll<PrimaryAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var userEmail = await repo.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).Select(a => a.PrimaryEMail).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            var destPath = model.FilePath;
            List<string> files = [model.FileName];

            _logger.LogInformation("SendEmailForPrimaryAllocationUploaded : MessageTemplate - " + @event.FileType.ToLower());
            IMessageTemplate? messageTemplate = null;

            if (string.Equals(@event.FileType, "agency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationAgencyUploadedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "telecallingagency", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationTCAgencyUploadedTemplate(model.CustomId, tenantId);
            }
            else if (string.Equals(@event.FileType, "allocationowner", StringComparison.OrdinalIgnoreCase))
            {
                messageTemplate = _messageTemplateFactory.PrimaryAllocationOwnerUploadedTemplate(model.CustomId, tenantId);
            }


            _logger.LogInformation("SendEmailForPrimaryAllocationUploaded : Send Email - " + userEmail);

            //Send mail with attachment
            await _emailUtility.SendEmailAsync(userEmail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId, files, destPath);

            _logger.LogInformation("SendEmailForPrimaryAllocationUploaded - : End");

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            // await this.Fire<SendEmailForPrimaryAllocationUploaded>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }
    }
}