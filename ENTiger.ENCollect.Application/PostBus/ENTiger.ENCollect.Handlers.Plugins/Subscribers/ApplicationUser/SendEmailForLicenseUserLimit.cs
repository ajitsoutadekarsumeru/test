using ENTiger.ENCollect.Messages.Events.License;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class SendEmailForLicenseUserLimit : ISendEmailForLicenseUserLimit
    {
        protected readonly ILogger<SendEmailForLicenseUserLimit> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IEmailUtility _emailUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        protected readonly ILicenseService _licenseService;
        protected readonly SystemUserSettings _systemUserSettings;

        public SendEmailForLicenseUserLimit(ILogger<SendEmailForLicenseUserLimit> logger, IRepoFactory repoFactory, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, ILicenseService licenseService, IOptions<SystemUserSettings> systemUserSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _licenseService = licenseService;
            _systemUserSettings = systemUserSettings.Value;
        }

        public virtual async Task Execute(UserLicenseLimitReachedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line

            _logger.LogInformation("SendEmailForLicenseUserLimit : Start");
            _repoFactory.Init(@event);
            var _tenantId = @event.AppContext.TenantId;
            List<GetUserTypeDetailsDto> userTypes = new();
            GetUserTypeDetailsDto? userTypeDetails;

            //Get FOS user type detail
            userTypeDetails = await _licenseService.GetUserTypeDetailAsync(UserTypeEnum.FOS, @event);
            userTypes.Add(userTypeDetails);
            //Get Telecaller user type detail
            userTypeDetails = await _licenseService.GetUserTypeDetailAsync(UserTypeEnum.Telecaller, @event);
            userTypes.Add(userTypeDetails);
            //Get Others user type detail
            userTypeDetails = await _licenseService.GetUserTypeDetailAsync(UserTypeEnum.Others, @event);
            userTypes.Add(userTypeDetails);

            SendLicenseUserLimitMassageDto message = new SendLicenseUserLimitMassageDto();
            message.UserType = @event.UserType;
            message.UserTypeDetails = userTypes;
            //TODO: get the correct username
            message.UserName = "SystemUser";
            
            var messageTemplate = _messageTemplateFactory.LicenseUserLimitEmailTemplate(message);
            _logger.LogInformation($"SendEmailForLicenseUserLimit : Email - {_systemUserSettings.SystemUserEmailId ?? "none"}");
            if (!String.IsNullOrEmpty(_systemUserSettings.SystemUserEmailId))
            {
                await _emailUtility.SendEmailAsync(_systemUserSettings.SystemUserEmailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
            }

            _logger.LogInformation("SendEmailForLicenseUserLimit : End");
        }
    }
}
