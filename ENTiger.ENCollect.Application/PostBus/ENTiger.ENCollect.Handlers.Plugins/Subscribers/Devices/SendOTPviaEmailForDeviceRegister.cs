using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class SendOTPviaEmailForDeviceRegister : ISendOTPviaEmailForDeviceRegister
    {
        protected readonly ILogger<SendOTPviaEmailForDeviceRegister> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendOTPviaEmailForDeviceRegister(ILogger<SendOTPviaEmailForDeviceRegister> logger, IRepoFactory repoFactory, ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(DeviceRegistered @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; 
            var repo = _repoFactory.Init(@event);

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
            var messageTemplate = _messageTemplateFactory.RegisterDeviceSendOTPEmailTemplate(devicedetaildto);

            _logger.LogInformation("Register Device : Email - " + applicationuser.PrimaryEMail);

            await _emailUtility.SendEmailAsync(applicationuser.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

            await this.Fire<SendOTPviaEmailForDeviceRegister>(EventCondition, serviceBusContext);
        }
    }
}