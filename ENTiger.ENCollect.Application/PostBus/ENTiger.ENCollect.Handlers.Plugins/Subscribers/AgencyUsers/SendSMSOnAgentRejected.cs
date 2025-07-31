using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendSMSOnAgentRejected : ISendSMSOnAgentRejected
    {
        protected readonly ILogger<SendSMSOnAgentRejected> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSOnAgentRejected(ILogger<SendSMSOnAgentRejected> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(AgentRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            List<string> agentIds = @event.Ids;

            foreach (var agentId in agentIds)
            {
                var agencyuserdto = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserId(agentId).SelectTo<AgencyUserDtoWithId>().FirstOrDefaultAsync();

                agencyuserdto.SetAppContext(@event.AppContext);

                var messageTemplate = _messageTemplateFactory.AgencyUserRejectedSMSTemplate(agencyuserdto);
                await _smsUtility.SendSMS(agencyuserdto.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);

            }
            await Task.CompletedTask;
        }
    }
}