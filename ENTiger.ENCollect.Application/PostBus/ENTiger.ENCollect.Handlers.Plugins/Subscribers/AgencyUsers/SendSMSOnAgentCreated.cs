using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendSMSOnAgentCreated : ISendSMSOnAgentCreated
    {
        protected readonly ILogger<SendSMSOnAgentCreated> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSOnAgentCreated(ILogger<SendSMSOnAgentCreated> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(AgentAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            var agencyuserdto = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserId(@event.Id).SelectTo<AgencyUserDtoWithId>().FirstOrDefaultAsync();
            agencyuserdto.SetAppContext(@event.AppContext);

            var messageTemplate = _messageTemplateFactory.AgencyUserCreatedSMSTemplate(agencyuserdto);
            await _smsUtility.SendSMS(agencyuserdto.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);

            await this.Fire<SendSMSOnAgentCreated>(EventCondition, serviceBusContext);
        }
    }
}