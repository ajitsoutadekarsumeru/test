using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendEMailOnAgentCreated : ISendEMailOnAgentCreated
    {
        protected readonly ILogger<SendEMailOnAgentCreated> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEMailOnAgentCreated(ILogger<SendEMailOnAgentCreated> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
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
            var messageTemplate = _messageTemplateFactory.AgencyUserCreatedEmailTemplate(agencyuserdto);

            _logger.LogInformation("SendEMailOnAgencyUserCreated : Email - " + agencyuserdto.PrimaryEMail);
            await _emailUtility.SendEmailAsync(agencyuserdto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            await Task.CompletedTask;
        }
    }
}