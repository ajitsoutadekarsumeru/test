using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class SendSMSForLicenseTransactionLimit : ISendSMSForLicenseTransactionLimit
    {
        protected readonly ILogger<SendSMSForLicenseTransactionLimit> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        protected readonly ILicenseService _licenseService;
        protected readonly IApplicationUserQueryRepository _userRepository;
        protected readonly SystemUserSettings _systemUserSettings;

        public SendSMSForLicenseTransactionLimit(ILogger<SendSMSForLicenseTransactionLimit> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, ILicenseService licenseService, IApplicationUserQueryRepository userRepository, IOptions<SystemUserSettings> systemUserSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _licenseService = licenseService;
            _userRepository = userRepository;
            _systemUserSettings = systemUserSettings.Value;
        }

        public virtual async Task Execute(TransactionLicenseLimitExceeded @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var _tenantId = @event.AppContext.TenantId;
            _repoFactory.Init(@event);
            _logger.LogInformation("SendSMSForLicenseTransactionLimit : Start");

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                .ByTFlexId(@event.UserId)
                .FirstOrDefaultAsync();

            List<GetUserTransactionLimitsDto> transactionTypes = new();
            GetUserTransactionLimitsDto? transactionTypeDetails;

            //Get Collections transaction type detail
            transactionTypeDetails = await _licenseService.GetTransactionTypeDetailAsync(LicenseTransactionType.Collections, "", @event);
            transactionTypes.Add(transactionTypeDetails);
            //Get Trails transaction type detail
            transactionTypeDetails = await _licenseService.GetTransactionTypeDetailAsync(LicenseTransactionType.Trails, "", @event);
            transactionTypes.Add(transactionTypeDetails);


            if (user != null)
            {
                SendLicenseTransactionLimitMassageDto message = new SendLicenseTransactionLimitMassageDto();
                message.TransactionType = @event.TransactionType;
                message.TransactionTypeDetails = transactionTypes;
                message.UserName = user.FirstName;
                message.Limit = transactionTypes.FirstOrDefault(x => x.TransactionType == @event.TransactionType)?.Limit ?? 0;

                //ServiceLayerTemplate
                var messageTemplate = _messageTemplateFactory.LicenseTransactionLimitEmailTemplate(message);

                _logger.LogInformation($"SendSMSForLicenseTransactionLimit : SMS - {user.PrimaryMobileNumber ?? "none"}");
                if (!String.IsNullOrEmpty(user.PrimaryMobileNumber))
                {
                    await _smsUtility.SendSMS(user.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);
                }
            }
            _logger.LogInformation("SendSMSForLicenseTransactionLimit : End");
        }
    }
}
