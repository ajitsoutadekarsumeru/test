using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForPrimaryUnAllocationUploaded : ISendEmailForPrimaryUnAllocationUploaded
    {
        protected readonly ILogger<SendEmailForPrimaryUnAllocationUploaded> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForPrimaryUnAllocationUploaded(ILogger<SendEmailForPrimaryUnAllocationUploaded> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PrimaryUnAllocationUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            var repo = _repoFactory.Init(@event);

            var model = await _repoFactory.GetRepo().FindAll<PrimaryUnAllocationFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

            var messageTemplate = _messageTemplateFactory.UnAllocationPrimaryUploadedTemplate(model.CustomId, model.UploadType, model.FileName, @event.AppContext.TenantId);
            _logger.LogInformation("PrimaryUnAllocationService : Email - " + user?.PrimaryEMail);

            await _emailUtility.SendEmailAsync(user?.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogInformation("PrimaryUnAllocationService : Send Email - End");

            await Task.CompletedTask;
        }
    }
}