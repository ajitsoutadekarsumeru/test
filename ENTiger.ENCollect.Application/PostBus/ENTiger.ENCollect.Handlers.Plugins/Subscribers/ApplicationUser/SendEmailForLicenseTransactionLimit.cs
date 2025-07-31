using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class SendEmailForLicenseTransactionLimit : ISendEmailForLicenseTransactionLimit
    {
        protected readonly ILogger<SendEmailForLicenseTransactionLimit> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly IEmailUtility _emailUtility;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        protected readonly ILicenseService _licenseService;
        protected readonly SystemUserSettings _systemUserSettings;

        public SendEmailForLicenseTransactionLimit(ILogger<SendEmailForLicenseTransactionLimit> logger, IRepoFactory repoFactory, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory, ILicenseService licenseService, IOptions<SystemUserSettings> systemUserSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _licenseService = licenseService;
            _systemUserSettings = systemUserSettings.Value;
        }

        public virtual async Task Execute(TransactionLicenseLimitExceeded @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line

            _logger.LogInformation("SendEmailForLicenseTransactionLimit : Start");
            _repoFactory.Init(@event);
            var _tenantId = @event.AppContext.TenantId;

            List<GetUserTransactionLimitsDto> transactionTypes = new();
            GetUserTransactionLimitsDto? transactionTypeDetails;

            //Get Collections transaction type detail
            transactionTypeDetails = await _licenseService.GetTransactionTypeDetailAsync(LicenseTransactionType.Collections, @event.UserId, @event);
            transactionTypes.Add(transactionTypeDetails);
            //Get Trails transaction type detail
            transactionTypeDetails = await _licenseService.GetTransactionTypeDetailAsync(LicenseTransactionType.Trails, @event.UserId, @event);
            transactionTypes.Add(transactionTypeDetails);

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                .ByTFlexId(@event.UserId)
                .FirstOrDefaultAsync();

            SendLicenseTransactionLimitMassageDto message = new SendLicenseTransactionLimitMassageDto();
            message.TransactionType = @event.TransactionType;
            message.TransactionTypeDetails = transactionTypes;
            message.UserName = user.FirstName;
            message.Limit = transactionTypes.FirstOrDefault(x => x.TransactionType == @event.TransactionType)?.Limit ?? 0;


            var messageTemplate = _messageTemplateFactory.LicenseTransactionLimitEmailTemplate(message);

            _logger.LogInformation($"SendEmailForLicenseTransactionLimit : Email - {user.PrimaryEMail ?? "none"}");
            if (!String.IsNullOrEmpty(user.PrimaryEMail))
            {
                await _emailUtility.SendEmailAsync(user.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
            }

            //TODO: Should we send to sysadmin as well?
            //message.UserName = "SystemUser";
            //messageTemplate = _messageTemplateFactory.LicenseTransactionLimitEmailTemplate(message);
            //_logger.LogInformation($"SendEmailForLicenseTransactionLimit : Email - {_systemUserSettings.SystemUserEmailId ?? "none"}");
            //if (!String.IsNullOrEmpty(_systemUserSettings.SystemUserEmailId))
            //{
            //    await _emailUtility.SendEmailAsync(_systemUserSettings.SystemUserEmailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, _tenantId);
            //}

            _logger.LogInformation("SendEmailForLicenseTransactionLimit : End");
        }
    }
}
