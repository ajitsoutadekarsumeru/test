using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SendEmailForPrimaryUnAllocationFailed : ISendEmailForPrimaryUnAllocationFailed
    {
        protected readonly ILogger<SendEmailForPrimaryUnAllocationFailed> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailForPrimaryUnAllocationFailed(ILogger<SendEmailForPrimaryUnAllocationFailed> logger,
            IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PrimaryUnAllocationFailedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("PrimaryUnAllocationFailedService : Start | JSON - " + JsonConvert.SerializeObject(@event));
            _flexAppContext = @event.AppContext; //do not remove this line
            string tenantId = _flexAppContext.TenantId;
            var repo = _repoFactory.Init(@event);

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.UnAllocationPrimaryFailedTemplate(@event.CustomId,@event.UnAllocationType, tenantId);
            _logger.LogInformation("PrimaryUnAllocationFailedService : Email - " + @event.Email);

            await _emailUtility.SendEmailAsync(@event.Email, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
            _logger.LogInformation("PrimaryUnAllocationFailedService : Send Email - End");

            _logger.LogInformation("PrimaryUnAllocationFailedService : End");
            //await this.Fire<SendEmailForPrimaryUnAllocationFailed>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }
    }
}