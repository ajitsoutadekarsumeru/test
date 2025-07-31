using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class SendEmailOnAgencyApproved : ISendEmailOnAgencyApproved
    {
        protected readonly ILogger<SendEmailOnAgencyApproved> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendEmailOnAgencyApproved(ILogger<SendEmailOnAgencyApproved> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(AgencyApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            foreach (var agencyId in @event.Ids)
            {
                var agencyuserdto = await _repoFactory.GetRepo().FindAll<Agency>().ByAgencyId(agencyId).SelectTo<AgencyDtoWithId>().FirstOrDefaultAsync();
                agencyuserdto.SetAppContext(@event.AppContext);
                var messageTemplate = _messageTemplateFactory.AgencyApprovedEmailTemplate(agencyuserdto);

                _logger.LogInformation("SendEmailOnAgencyApproved : Email - " + agencyuserdto.PrimaryEMail);
                await _emailUtility.SendEmailAsync(agencyuserdto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
            }
            await this.Fire<SendEmailOnAgencyApproved>(EventCondition, serviceBusContext);
        }
    }
}