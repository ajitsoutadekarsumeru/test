using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class SendSMSOnCompanyUserRejected : ISendSMSOnCompanyUserRejected
    {
        protected readonly ILogger<SendSMSOnCompanyUserRejected> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendSMSOnCompanyUserRejected(ILogger<SendSMSOnCompanyUserRejected> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(CompanyUserRejected @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            List<string> companyuserids = @event.Id.ToList();
            //TODO: Write your business logic here:
            foreach (var companyuserid in companyuserids)
            {
                var companyuserdto = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .ByCompanyUserId(companyuserid)
                                            .SelectTo<CompanyUserDtoWithId>().FirstOrDefaultAsync();
                companyuserdto.SetAppContext(@event.AppContext);

                var messageTemplate = _messageTemplateFactory.CompanyUserRejectedSMSTemplate(companyuserdto);
                await _smsUtility.SendSMS(companyuserdto.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);
            }
            await Task.CompletedTask;
        }
    }
}