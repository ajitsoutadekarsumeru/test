using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class SendEmailOnCompanyUserMadeDormant  : ISendEmailOnCompanyUserMadeDormant
    {
        protected readonly ILogger<SendEmailOnCompanyUserMadeDormant> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IMapper _mapper;
        private readonly SystemUserSettings _systemUserSettings;

        public SendEmailOnCompanyUserMadeDormant(ILogger<SendEmailOnCompanyUserMadeDormant> logger, ICustomUtility customUtility, IRepoFactory repoFactory
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

        public virtual async Task Execute(CompanyUserDormant @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);

            foreach (var userId in @event.Ids)
            {
                var companyUser = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                    .ByCompanyUserId(userId)
                    .FlexInclude("SinglePointReportingManager")
                    .FirstOrDefaultAsync();
                var companyUserDto = _mapper.Map<CompanyUserDto>(companyUser);

                companyUserDto.SetAppContext(@event.AppContext);

                //get user email address
                //get manager email address
                //get manager name
                string ManagerName = companyUser.SinglePointReportingManager?.FirstName ?? "Manager";

                _logger.LogDebug("CompanyUserDormantEmailService : Send Email");

                var messageTemplate = _messageTemplateFactory.MakeDormantCompanyUserEmailTemplate(companyUserDto, _systemUserSettings.UserInactivityDormantDays);
                var messageTemplateManager = _messageTemplateFactory.MakeDormantCompanyUserManagerEmailTemplate(companyUserDto, _systemUserSettings.UserInactivityDormantDays, ManagerName);

                
                if (!String.IsNullOrEmpty(companyUserDto.PrimaryEMail))
                {
                    _logger.LogInformation($"SendEmailOnCompanyUserMadeDormant : User Email - {companyUserDto.PrimaryEMail}");
                    var task1 = await _emailUtility.SendEmailAsync(companyUserDto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);
                }
                
                if (!String.IsNullOrEmpty(companyUser.SinglePointReportingManager?.PrimaryEMail))
                {
                    _logger.LogInformation($"SendEmailOnCompanyUserMadeDormant : Manager Email - {companyUser.SinglePointReportingManager?.PrimaryEMail}");
                    var task2 = await _emailUtility.SendEmailAsync(companyUser.SinglePointReportingManager?.PrimaryEMail, messageTemplateManager.EmailMessage, messageTemplateManager.EmailSubject, @event.AppContext.TenantId);
                }

                _logger.LogDebug("CompanyUserDormantEmailService : Emails Sent Successfully.");
            }
        }
    }
}