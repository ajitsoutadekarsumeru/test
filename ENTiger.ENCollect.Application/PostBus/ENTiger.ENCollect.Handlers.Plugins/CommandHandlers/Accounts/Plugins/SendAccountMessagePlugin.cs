using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendAccountMessagePlugin : FlexiPluginBase, IFlexiPlugin<SendAccountMessagePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a16655ed191b890a7a1b2e3a285db75";
        public override string FriendlyName { get; set; } = "SendAccountMessagePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SendAccountMessagePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public SendAccountMessagePlugin(ILogger<SendAccountMessagePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
            , ISmsUtility smsUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _messageTemplateFactory = messageTemplateFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(SendAccountMessagePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            SendAccountMessageDto model = new SendAccountMessageDto();
            model = packet.Cmd.Dto;

            switch (model.Template.ToLower())
            {
                case "appointment reminder":
                    await sendappointmentsms(model);
                    break;

                case "online payment link":
                    await sendonlinepaymentlinksmsAsync(model);
                    break;
            }
            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
            await Task.CompletedTask;
        }

        private async Task sendappointmentsms(SendAccountMessageDto model)
        {
            _logger.LogInformation("SendAccountMessagePlugin : SendAppointmentSms - Start");
            var account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => x.Id == model.AccountId)
                            .Select(p => new
                            {
                                p.Id,
                                p.MAILINGMOBILE,
                                p.Feedbacks
                            }).FirstOrDefaultAsync();

            if (account != null)
            {
                model.ContactNumber = account.MAILINGMOBILE;

                DateTime todaydate = DateTime.Now;

                var _ptpdate = account.Feedbacks.Where(x => x.DispositionCode == "PTP" && x.PTPDate > todaydate)
                                .OrderBy(x => x.FeedbackDate).Select(a => a.PTPDate).FirstOrDefault();

                string date = string.Empty;
                if (_ptpdate != null)
                {
                    date = string.Format("{0:dd MMMM yyyy}", _ptpdate);
                }

                var dto = new AccountMessageDto
                {
                    Date = date
                };
                dto.SetAppContext(model.GetAppContext());
                var messageTemplate = _messageTemplateFactory.AccountPTPTemplate(dto);

                _logger.LogInformation("SendAccountMessagePlugin : MobileNumber - " + model.ContactNumber);
                if (!string.IsNullOrEmpty(model.ContactNumber))
                {
                    await _smsUtility.SendSMS(model.ContactNumber, messageTemplate.SMSMessage, model.GetAppContext().TenantId);
                }
            }
            _logger.LogInformation("SendAccountMessagePlugin : SendAppointmentSms - End");
        }

        private async Task sendonlinepaymentlinksmsAsync(SendAccountMessageDto model)
        {
            _logger.LogInformation("SendAccountMessagePlugin : SendOnlinePaymentLink - Start");
            var account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => x.Id == model.AccountId)
                            .Select(p => new
                            {
                                p.Id,
                                p.MAILINGMOBILE
                            }).FirstOrDefaultAsync();

            if (account != null)
            {
                model.ContactNumber = account.MAILINGMOBILE;

                var dto = new AccountMessageDto { };
                dto.SetAppContext(model.GetAppContext());
                var messageTemplate = _messageTemplateFactory.AccountPaymentTemplate(dto);

                _logger.LogInformation("SendAccountMessagePlugin : MobileNumber - " + model.ContactNumber);
                if (!string.IsNullOrEmpty(model.ContactNumber))
                {
                    await _smsUtility.SendSMS(model.ContactNumber, messageTemplate.SMSMessage, model.GetAppContext().TenantId);
                }
            }
            _logger.LogInformation("SendAccountMessagePlugin : SendOnlinePaymentLink - End");
        }
    }
}