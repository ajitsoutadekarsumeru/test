using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendEmailOnAgencyUserMadeDormant  : ISendEmailOnAgencyUserMadeDormant
    {
        protected readonly ILogger<SendEmailOnAgencyUserMadeDormant> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IMapper _mapper;
        private readonly SystemUserSettings _systemUserSettings;

        public SendEmailOnAgencyUserMadeDormant(ILogger<SendEmailOnAgencyUserMadeDormant> logger, ICustomUtility customUtility, IRepoFactory repoFactory
            , ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory
            , IOptions<AuthSettings> authSettings, IApiHelper apiHelper, IOptions<SystemUserSettings> systemUserSettings, IMapper mapper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _systemUserSettings = systemUserSettings.Value;
            _mapper = mapper;
        }

        public virtual async Task Execute(AgentDormant @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            foreach (var agentId in @event.Ids)
            {
                var agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                    .ByAgencyUserId(agentId)
                    .FlexInclude("Agency")
                    .FirstOrDefaultAsync();
                var agencyUserManager = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgentEmail(agencyUser.SupervisorEmailId).FirstOrDefaultAsync();
                var agencyUserDto = _mapper.Map<AgencyUserDto>(agencyUser);

                agencyUserDto.SetAppContext(@event.AppContext);

                //get user email address
                //get manager email address
                //get manager name
                //get agency email address
                string ManagerName = agencyUserManager?.FirstName ?? "Manager";
                string agencyMails = "";
                List<string> mailList = new List<string>();
                if (!String.IsNullOrEmpty(agencyUser.Agency.PrimaryEMail)) mailList.Add(agencyUser.Agency.PrimaryEMail);
                if (!String.IsNullOrEmpty(agencyUser.SupervisorEmailId)) mailList.Add(agencyUser.SupervisorEmailId);
                if (mailList.Count > 0) agencyMails = string.Join(",", mailList);

                _logger.LogDebug("AgencyUserDormantEmailService : Send Email");

                var messageTemplate = _messageTemplateFactory.MakeDormantAgencyUserEmailTemplate(agencyUserDto, _systemUserSettings.UserInactivityDormantDays);
                var messageTemplateManager = _messageTemplateFactory.MakeDormantAgencyUserManagerEmailTemplate(agencyUserDto, _systemUserSettings.UserInactivityDormantDays, ManagerName);
                if (!String.IsNullOrEmpty(agencyUserDto.PrimaryEMail))
                {
                    _logger.LogInformation($"SendEmailOnAgencyUserMadeDormant : User Email - {agencyUserDto.PrimaryEMail}");
                    var task1 = await _emailUtility.SendEmailAsync(agencyUserDto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
                }
                if (!String.IsNullOrEmpty(agencyMails))
                {
                    _logger.LogInformation($"SendEmailOnAgencyUserMadeDormant : Manager and Agency Email - {agencyMails}");
                    var task2 = await _emailUtility.SendEmailAsync(agencyMails, messageTemplateManager.EmailMessage, messageTemplateManager.EmailSubject, @event.AppContext.TenantId);
                }

                _logger.LogDebug("AgencyUserDormantEmailService : Emails Sent Successfully.");
            }
        }
    }
}