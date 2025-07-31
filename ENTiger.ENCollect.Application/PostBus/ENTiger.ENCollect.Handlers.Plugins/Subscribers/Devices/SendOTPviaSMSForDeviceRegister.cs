using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class SendOTPviaSMSForDeviceRegister : ISendOTPviaSMSForDeviceRegister
    {
        protected readonly ILogger<SendOTPviaSMSForDeviceRegister> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendOTPviaSMSForDeviceRegister(ILogger<SendOTPviaSMSForDeviceRegister> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(DeviceRegistered @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            //TODO: Write your business logic here:

            var devicedetaildto = await _repoFactory.GetRepo().FindAll<DeviceDetail>()
                                        .Where(a => a.Id == @event.Id)
                                        .SelectTo<DeviceDetailDtoWithId>()
                                        .FirstOrDefaultAsync();

            var applicationuser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(a => a.Id == devicedetaildto.UserId)
                                        .FirstOrDefaultAsync();

            devicedetaildto.FirstName = applicationuser.FirstName;
            devicedetaildto.LastName = applicationuser.LastName;
            devicedetaildto.SetAppContext(@event.AppContext);
            var messageTemplate = _messageTemplateFactory.RegisterDeviceSendOTPSMSTemplate(devicedetaildto);

            _logger.LogInformation("Register Device : SMS - " + applicationuser.PrimaryMobileNumber);

            await _smsUtility.SendSMS(applicationuser.PrimaryMobileNumber, messageTemplate.SMSMessage, @event.AppContext.TenantId);

            await this.Fire<SendOTPviaSMSForDeviceRegister>(EventCondition, serviceBusContext);
        }
    }
}