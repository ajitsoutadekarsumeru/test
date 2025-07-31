using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentCopyViaSMSPlugin : FlexiPluginBase, IFlexiPlugin<SendPaymentCopyViaSMSPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138ee17deae63c6bcf727fc61cba08";
        public override string FriendlyName { get; set; } = "SendPaymentCopyViaSMSPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SendPaymentCopyViaSMSPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Collection? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendPaymentCopyViaSMSPlugin(ILogger<SendPaymentCopyViaSMSPlugin> logger,
            IFlexHost flexHost, IRepoFactory repoFactory, ISmsUtility smsUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _smsUtility = smsUtility;

            _messageTemplateFactory = messageTemplateFactory;
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SendPaymentCopyViaSMSPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            var repo = _repoFactory.Init(packet.Cmd.Dto);
            //DuplicateReceiptNotification
            var collectionDto = await repo.GetRepo().FindAll<Collection>()
                                      .ByCollectionId(packet.Cmd.Dto.paymentId)
                                      .SelectTo<CollectionDtoWithId>()
                                      .FirstOrDefaultAsync();
            collectionDto.SetAppContext(_flexAppContext);
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.DuplicateReceiptTemplate(collectionDto, packet.Cmd.Dto.GetAppContext().TenantId);

            _logger.LogInformation("SendPaymentCopyViaSMSPlugin : SMS - " + collectionDto.MobileNo);

            await _smsUtility.SendSMS(collectionDto.MobileNo, messageTemplate.SMSMessage, packet.Cmd.Dto.GetAppContext().TenantId);
            _logger.LogInformation("SendPaymentCopyViaSMSPlugin : Send SMS - End");
        }
    }
}