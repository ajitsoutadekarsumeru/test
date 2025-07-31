using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendEmailOnAgencyCreated : ISendEmailOnAgencyCreated
    {
        protected readonly ILogger<SendEmailOnAgencyCreated> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailOnAgencyCreated(ILogger<SendEmailOnAgencyCreated> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(AgencyCreatedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            var agencycreateddto = await _repoFactory.GetRepo().FindAll<Agency>().ByAgencyId(@event.Id).SelectTo<AgencyDtoWithId>().FirstOrDefaultAsync();

            agencycreateddto.SetAppContext(@event.AppContext);
            var messageTemplate = _messageTemplateFactory.AgencyCreatedEmailTemplate(agencycreateddto);

            _logger.LogInformation("SendEMailOnAgencyCreated : Email - " + agencycreateddto.PrimaryEMail);
            await _emailUtility.SendEmailAsync(agencycreateddto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            await this.Fire<SendEmailOnAgencyCreated>(EventCondition, serviceBusContext);
        }
    }
}