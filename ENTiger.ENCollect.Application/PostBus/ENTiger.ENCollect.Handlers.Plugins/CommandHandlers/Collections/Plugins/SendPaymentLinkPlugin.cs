using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentLinkPlugin : FlexiPluginBase, IFlexiPlugin<SendPaymentLinkPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138ee3040531048e7a0bec397d4b8d";
        public override string FriendlyName { get; set; } = "SendPaymentLinkPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SendPaymentLinkPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Collection? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected string paymentPartner = string.Empty;
        protected AccountContactHistoryEventData _contactHistoryData;

        public SendPaymentLinkPlugin(ILogger<SendPaymentLinkPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SendPaymentLinkPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            paymentPartner = packet.Cmd.Dto.OnlinePayPartnerName;
            var account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(i => i.AGREEMENTID == packet.Cmd.Dto.Accountno).FirstOrDefaultAsync();

            _model = _flexHost.GetDomainModel<Collection>().SendPaymentLink(packet.Cmd, account.Id, account.CUSTOMERNAME);

            AttachReceiptToCollection(_model, packet.Cmd.CustomId);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database: ", typeof(Collection).Name, _model.Id);

                EventCondition = CONDITION_ONSUCCESS;

                await AddAccountContactHistoryAsync(packet, _model);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Collection).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task AddAccountContactHistoryAsync(SendPaymentLinkPostBusDataPacket packet, Collection collection)
        {
            string mobileNo = collection.MobileNo;
            string emailId = collection.EMailId;

            // Skip if all three are null or empty
            if (string.IsNullOrWhiteSpace(mobileNo) &&
                string.IsNullOrWhiteSpace(emailId))
            {
                _logger.LogDebug("Skipping AccountContactHistoryEvent — all contact fields are empty.");
                return;
            }

            _contactHistoryData = new AccountContactHistoryEventData(
                                ContactSource: ContactSourceEnum.SendPaymentLink.ToString(),
                                EmailId: emailId,
                                MobileNo: mobileNo,
                                Address: null,
                                AccountId: collection.AccountId,
                                Latitude: decimal.TryParse(collection.Latitude, out var lat) ? lat : (decimal?)null,
                                Longitude: decimal.TryParse(collection.Longitude, out var lon) ? lon : (decimal?)null
                            );

            string EventContactHistoryCondition = CONDITION_OnContactHistoryAddRequest;
            await this.Fire(EventContactHistoryCondition, packet.FlexServiceBusContext);
        }

        private void AttachReceiptToCollection(Collection collection, string receiptNo)
        {
            _logger.LogDebug("Physical Receipt: Attach Receipt To Collection");
            Receipt receipt = new Receipt();
            receipt.CustomId = receiptNo;
            receipt.CollectorId = collection.CreatedBy;
            receipt.SetAdded();
            receipt.ReceiptWorkflowState = _flexHost.GetFlexStateInstance<CollectionCollectedByCollector>().SetTFlexId(receipt.Id).SetStateChangedBy(collection.CreatedBy ?? "");

            collection.Receipt = receipt;
            collection.ReceiptId = receipt.Id;
            collection.SetCustomId(receiptNo);
        }
    }
}