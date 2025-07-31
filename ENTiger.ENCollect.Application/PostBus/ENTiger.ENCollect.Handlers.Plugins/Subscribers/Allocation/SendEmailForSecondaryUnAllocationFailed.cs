using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForSecondaryUnAllocationFailed : ISendEmailForSecondaryUnAllocationFailed
    {
        protected readonly ILogger<SendEmailForSecondaryUnAllocationFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private string _unAllocationType;

        public SendEmailForSecondaryUnAllocationFailed(ILogger<SendEmailForSecondaryUnAllocationFailed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SecondaryUnAllocationFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            string tenantId = _flexAppContext.TenantId;
            var repo = _repoFactory.Init(@event);
            _unAllocationType = @event?.UnAllocationType;

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.UnAllocationSecondaryFailedTemplate(@event.CustomId, @event.AppContext.TenantId, _unAllocationType);
            _logger.LogInformation("SecondaryUnAllocationFailedService : Email - " + @event.Email);

            await _emailUtility.SendEmailAsync(@event.Email, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            _logger.LogInformation("SecondaryUnAllocationFailedService : Send Email - End");

            //await this.Fire<SendEmailForSecondaryUnAllocationFailed>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }
    }
}