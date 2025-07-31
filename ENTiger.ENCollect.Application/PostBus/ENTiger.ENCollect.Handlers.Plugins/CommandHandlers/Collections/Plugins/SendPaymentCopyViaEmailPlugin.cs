using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentCopyViaEmailPlugin : FlexiPluginBase, IFlexiPlugin<SendPaymentCopyViaEmailPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138ee0172689778fc2f20527d9f575";
        public override string FriendlyName { get; set; } = "SendPaymentCopyViaEmailPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SendPaymentCopyViaEmailPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Collection? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IEmailUtility _emailUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;

        public SendPaymentCopyViaEmailPlugin(ILogger<SendPaymentCopyViaEmailPlugin> logger,
            IFlexHost flexHost, IRepoFactory repoFactory, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;

            _messageTemplateFactory = messageTemplateFactory;
        }

        public virtual async Task Execute(SendPaymentCopyViaEmailPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            var repo = _repoFactory.Init(packet.Cmd.Dto);

            //DuplicateReceiptNotification
            var collectionDto = await repo.GetRepo().FindAll<Collection>().ByCollectionId(packet.Cmd.Dto.paymentId)
                                    .SelectTo<CollectionDtoWithId>().FirstOrDefaultAsync();
            collectionDto.SetAppContext(_flexAppContext);
            //ServiceLayerTemplate
            var messageTemplate = _messageTemplateFactory.DuplicateReceiptTemplate(collectionDto, packet.Cmd.Dto.GetAppContext().TenantId);

            _logger.LogInformation("SendPaymentCopyViaEmailPlugin : Email - " + collectionDto.EMailId);

            await _emailUtility.SendEmailAsync(collectionDto.EMailId, messageTemplate.EmailMessage, messageTemplate.EmailSubject, packet.Cmd.Dto.GetAppContext().TenantId);
            _logger.LogInformation("SendPaymentCopyViaEmailPlugin : Send Email - End");
        }
    }
}