using ENCollect.Security;
using ENTiger.ENCollect.CollectionsModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddFeedbackPlugin : FlexiPluginBase, IFlexiPlugin<AddFeedbackPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1365b6fa08c9f19cb71e199eb58521";
        public override string FriendlyName { get; set; } = "AddFeedbackPlugin";
        protected string EventCondition = "";
        protected readonly ILogger<AddFeedbackPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected Feedback? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly EncryptionSettings _encryptionSettings;
        protected AccountContactHistoryEventData _contactHistoryData;

        public AddFeedbackPlugin(ILogger<AddFeedbackPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<EncryptionSettings> encryptionSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _encryptionSettings = encryptionSettings.Value;

        }

        public virtual async Task Execute(AddFeedbackPostBusDataPacket packet)
        {
            _logger.LogInformation("AddFeedbackPlugin : Start");
            string key = _encryptionSettings.StaticKeys.DecryptionKey;
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            _logger.LogInformation("AddFeedbackPlugin : Start  UserId : " + _flexAppContext?.UserId);
            AddFeedbackDto inputdto = packet.Cmd.Dto;
            _model = _flexHost.GetDomainModel<Feedback>().AddFeedback(packet.Cmd);
            var aesGcmCrypto = new AesGcmCrypto();
            var aesGcmKey = Encoding.UTF8.GetBytes(key);
            inputdto.Accountno = aesGcmCrypto.Decrypt(inputdto.Accountno, aesGcmKey);
            _model.AccountId = await FetchAccountIdAsync(inputdto.Accountno);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(Feedback).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;

                await AddAccountContactHistoryAsync(packet, _model);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(Feedback).Name, _model.Id);
            }

            _logger.LogInformation("AddFeedbackPlugin : Start | EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
        private async Task AddAccountContactHistoryAsync(AddFeedbackPostBusDataPacket packet, Feedback feedback)
        {
            // Construct values
            string mobileNo = feedback.NewContactNo;
            string emailId = feedback.NewEmailId;
            string address = string.Join(", ", new[] { feedback.NewArea, feedback.NewAddress, feedback.State, feedback.City }
.Where(s => !string.IsNullOrWhiteSpace(s)));

            // Skip if all three are null or empty
            if (string.IsNullOrWhiteSpace(mobileNo) &&
                string.IsNullOrWhiteSpace(emailId) &&
                string.IsNullOrWhiteSpace(address))
            {
                _logger.LogDebug("Skipping AccountContactHistoryEvent — all contact fields are empty.");
                return;
            }

            _contactHistoryData = new AccountContactHistoryEventData(
                                ContactSource: ContactSourceEnum.Trail.ToString(),
                                EmailId: emailId,
                                MobileNo: mobileNo,
                                Address: address,
                                AccountId: feedback.AccountId,
                               Latitude: feedback.Latitude.HasValue ? (decimal?)feedback.Latitude.Value : null,
                               Longitude: feedback.Longitude.HasValue ? (decimal?)feedback.Longitude.Value : null
                            );

            string EventContactHistoryCondition = CONDITION_OnContactHistoryAddRequest;
            await this.Fire(EventContactHistoryCondition, packet.FlexServiceBusContext);
        }
        private async Task<string?> FetchAccountIdAsync(string accountNo)
        {
            _logger.LogInformation("AddFeedbackFFPlugin : FetchAccountId - Start | AccountNo - " + accountNo);
            string? accountId;

            accountId = await _repoFactory.GetRepo().FindAll<LoanAccount>().ByAccountNo(accountNo).Select(a => a.Id).FirstOrDefaultAsync();

            _logger.LogInformation("AddFeedbackFFPlugin : FetchAccountId - Start | AccountId - " + accountId);
            return accountId;
        }
    }
}