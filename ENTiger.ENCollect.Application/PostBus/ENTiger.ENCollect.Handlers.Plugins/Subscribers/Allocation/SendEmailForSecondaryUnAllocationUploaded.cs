using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForSecondaryUnAllocationUploaded : ISendEmailForSecondaryUnAllocationUploaded
    {
        protected readonly ILogger<SendEmailForSecondaryUnAllocationUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForSecondaryUnAllocationUploaded(ILogger<SendEmailForSecondaryUnAllocationUploaded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SecondaryUnAllocationUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            var model = await _repoFactory.GetRepo().FindAll<SecondaryUnAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.UnAllocationSecondaryUploadedTemplate(model.CustomId, model.UploadType, model.FileName, @event.AppContext.TenantId);
            _logger.LogInformation("Secondary UnAllocationService : Email - " + user?.PrimaryEMail);

            await _emailUtility.SendEmailAsync(user?.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogInformation("SecondaryUnAllocationService : Send Email - End");
        }
    }
}