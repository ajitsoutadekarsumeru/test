using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Web;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendVideoCallLinkPlugin : FlexiPluginBase, IFlexiPlugin<SendVideoCallLinkPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13f6f95fda88b6b1bdc3bf9daa4c14";
        public override string FriendlyName { get; set; } = "SendVideoCallLinkPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SendVideoCallLinkPlugin> _logger;
        protected readonly IFlexHost _flexHost;

        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly IApiHelper _apiHelper;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="logger"></param>
        public SendVideoCallLinkPlugin(ILogger<SendVideoCallLinkPlugin> logger, IFlexHost flexHost,
            IEmailUtility emailUtility, ISmsUtility smsUtility, MessageTemplateFactory messageTemplateFactory, IApiHelper apiHelper)
        {
            _logger = logger;
            _flexHost = flexHost;
            _emailUtility = emailUtility;
            _smsUtility = smsUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _apiHelper = apiHelper;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="packet"></param>
        public virtual async Task Execute(SendVideoCallLinkPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            string tenantId = _flexAppContext.TenantId;

            string customerName = packet.Cmd.Dto.CustomerName;

            string link = packet.Cmd.Dto.Link;
            string email = packet.Cmd.Dto.EmailId;
            string mobileNo = packet.Cmd.Dto.MobileNumber;
            string tinylink = await GetTinyUrl(link);

            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.VideoTemplate(customerName, link, tinylink, tenantId);

            _logger.LogInformation("SendVideoCallLinkPlugin : Email - " + packet.Cmd.Dto.MobileNumber);
            await _emailUtility.SendEmailAsync(email, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
            _logger.LogInformation("SendVideoCallLinkPlugin : Send Email - End");

            _logger.LogInformation("SendPaymentCopyViaSMSPlugin : SMS - " + mobileNo);
            await _smsUtility.SendSMS(mobileNo, messageTemplate.SMSMessage, tenantId);
            _logger.LogInformation("SendPaymentCopyViaSMSPlugin : Send SMS - End");

            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);
            await Task.CompletedTask;
        }

        private async Task<string> GetTinyUrl(string url)
        {
            _logger.LogInformation("SendVideoCallLinkPlugin : GetTinyUrl - Start | Url - " + url);
            string tinyUrl = string.Empty;
            try
            {
                string address = $"http://tinyurl.com/api-create.php?url={url}";
                var response = await _apiHelper.SendRequestAsync("", address, HttpMethod.Get);
                tinyUrl = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in SendVideoCallLinkPlugin : GetTinyUrl - " + ex);
            }
            _logger.LogInformation("SendVideoCallLinkPlugin : GetTinyUrl - End | TinyUrl - " + tinyUrl);
            return tinyUrl;
        }
    }
}