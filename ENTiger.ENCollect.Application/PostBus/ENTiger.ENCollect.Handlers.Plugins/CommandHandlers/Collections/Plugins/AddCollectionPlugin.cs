using ENCollect.Security;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionPlugin : FlexiPluginBase, IFlexiPlugin<AddCollectionPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138e3c26e7a945a74eee26f7d0a1bf";
        public override string FriendlyName { get; set; } = "AddCollectionPlugin";
        protected string EventCondition = "";
        protected readonly ILogger<AddCollectionPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly EncryptionSettings _encryptionSettings;

        protected Collection? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected string _reservationId = string.Empty;
        protected AccountContactHistoryEventData _contactHistoryData;

        public AddCollectionPlugin(ILogger<AddCollectionPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
            , IOptions<EncryptionSettings> encryptionSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _encryptionSettings = encryptionSettings.Value;
        }

        public virtual async Task Execute(AddCollectionPostBusDataPacket packet)
        {
            var input = packet.Cmd.Dto;
            _logger.LogInformation("AddCollectionPlugin : Start | ReceiptNo - " + input.ReceiptNo + " | DateTime - " + DateTime.Now);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _flexAppContext = input.GetAppContext();  //do not remove this line
            string loggedInUserId = _flexAppContext.UserId;
            _reservationId = packet.Cmd.ReservationId;

            string accountNo = input.AccountNo;
            string receiptNo = input.ReceiptNo;
            var aesGcmCrypto = new AesGcmCrypto();
            string key = _encryptionSettings.StaticKeys.DecryptionKey;
            var aesGcmKey = Encoding.UTF8.GetBytes(key);
            accountNo = aesGcmCrypto.Decrypt(accountNo, aesGcmKey);
          
            LoanAccount account = await FetchAccountAsync(accountNo);

            _model = _flexHost.GetDomainModel<Collection>().AddCollection(packet.Cmd);

            _model.SetAccount(account.Id);
            _model.SetCollector(loggedInUserId);

            var loggedinUserAccountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a => a.ResponsibleId == loggedInUserId).FirstOrDefaultAsync();
            _model.CollectionOrgId = loggedinUserAccountability?.CommisionerId;

            _model.TransactionSource = _flexAppContext?.RequestSource;
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();

            if (records > 0)
            {
                _logger.LogInformation("AddCollectionPlugin : End | ReceiptNo - " + input.ReceiptNo + " | DateTime - " + DateTime.Now);
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Collection).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;

                await AddAccountContactHistoryAsync(packet, _model);
            }
            else
            {
                _logger.LogWarning("AddCollectionPlugin : Error | ReceiptNo - " + input.ReceiptNo + " | DateTime - " + DateTime.Now);
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Collection).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task AddAccountContactHistoryAsync(AddCollectionPostBusDataPacket packet, Collection collection)
        {
            string mobileNo = collection.MobileNo;
            string emailId = collection.EMailId;

            // Skip if all two are null or empty
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
            _logger.LogDebug("AddCollectionPlugin : FetchAccountId - Start | AccountNo - " + accountNo);
            LoanAccount account;

            account = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(i => i.AGREEMENTID == accountNo || i.CustomId == accountNo).FirstOrDefaultAsync();

            _logger.LogDebug("AddCollectionPlugin : FetchAccountId - End | AccountId - " + account.Id);
            return account;
        }
    }
}