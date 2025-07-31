using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddPhysicalCollectionPlugin : FlexiPluginBase, IFlexiPlugin<AddPhysicalCollectionPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138e3fc6cd2dc7d5dd692bed0e9fcd";
        public override string FriendlyName { get; set; } = "AddPhysicalCollectionPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddPhysicalCollectionPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Collection? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string userid;
        protected AccountContactHistoryEventData _contactHistoryData;

        public AddPhysicalCollectionPlugin(ILogger<AddPhysicalCollectionPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddPhysicalCollectionPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();//do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            userid = _flexAppContext.UserId;

            var model = packet.Cmd.Dto;

            string accountNo = model.AccountNo;
            string receiptNo = packet.Cmd.CustomId;

            LoanAccount account = await FetchAccountAsync(accountNo);

            _model = _flexHost.GetDomainModel<Collection>().AddPhysicalCollection(packet.Cmd, account);
            AttachReceiptToCollection(userid, _model, receiptNo);
            _model.MarkAsAcknowledged(userid);
            _model.Status = CollectionStatusEnum.withAgency_Or_Branch.Value;
            _model.CustomerName = account?.CUSTOMERNAME;

            var loggedinUserAccountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == userid).FirstOrDefaultAsync();
            _model.CollectionOrgId = loggedinUserAccountability?.CommisionerId;
            _model.SetCollector(userid);

            _model.TransactionSource = _flexAppContext?.RequestSource;
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Collection).Name, _model.Id);
                if (string.Equals(_model.CollectionMode, CollectionModeEnum.Online.Value, StringComparison.OrdinalIgnoreCase))
                {
                    EventCondition = CONDITION_ONONLINEPAYMENT;
                }
                else
                {
                    _logger.LogInformation($"CollectorId : {_model.CollectorId}");
                    EventCondition = CONDITION_ONSUCCESS;
                }
                await AddAccountContactHistoryAsync(packet, _model);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Collection).Name, _model.Id);
            }
            _logger.LogInformation("AddPhysicalCollectionPlugin : EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
        private async Task AddAccountContactHistoryAsync(AddPhysicalCollectionPostBusDataPacket packet, Collection collection)
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
                                ContactSource: ContactSourceEnum.Receipt.ToString(),
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

        private async Task<LoanAccount> FetchAccountAsync(string accountNo)
        {
            _logger.LogInformation("AddPhysicalCollectionPlugin : FetchAccount");
            LoanAccount account = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                    .Where(i => i.AGREEMENTID == accountNo || i.CustomId == accountNo || i.PRIMARY_CARD_NUMBER == accountNo).FirstOrDefaultAsync();

            return account;
        }

        private void AttachReceiptToCollection(string collectorId, Collection collection, string receiptNo)
        {
            _logger.LogInformation("AddPhysicalCollectionPlugin: Attach Receipt To Collection");
            //GenerateNewReceipt
            Receipt receipt = new Receipt();
            receipt.CustomId = receiptNo;
            receipt.CollectorId = collectorId;
            receipt.SetAdded();
            receipt.MarkAsCollectionCollectedByCollector(userid);

            collection.Receipt = receipt;
            collection.ReceiptId = receipt.Id;
            collection.CustomId = receiptNo;
        }
    }
}