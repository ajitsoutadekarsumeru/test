using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountContactHistoryModule
{
    public partial class AddAccountContactHistoryPlugin : FlexiPluginBase, IFlexiPlugin<AddContactHistoryPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13373c8c29e95a9e48a9b425738cf5";
        public override string FriendlyName { get; set; } = "AddAccountContactHistoryPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddAccountContactHistoryPlugin> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ILoanAccountQueryRepository _loanAccountRepository;
        protected readonly ILoanAccountContactHistoryQueryRepository _loanAccountContactHistoryQueryRepository;

        protected LoanAccount? _loanAccount;
        protected FlexAppContextBridge? _flexAppContext;

        public AddAccountContactHistoryPlugin(ILogger<AddAccountContactHistoryPlugin> logger, IRepoFactory repoFactory, ILoanAccountQueryRepository loanAccountQueryRepository, ILoanAccountContactHistoryQueryRepository loanAccountContactHistoryQueryRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _loanAccountRepository = loanAccountQueryRepository;
            _loanAccountContactHistoryQueryRepository = loanAccountContactHistoryQueryRepository;
        }

        public virtual async Task Execute(AddContactHistoryPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.flexAppContext;  //do not remove this line
            _repoFactory.Init(_flexAppContext);

            var validSources = new[]
               {
                   ContactSourceEnum.AccountImport.ToString(),
                   ContactSourceEnum.Trail.ToString(),
                   ContactSourceEnum.Receipt.ToString(),
                   ContactSourceEnum.SendPaymentLink.ToString()
               };

            // Skip processing if source is missing or not allowed
            if (!validSources.Contains(packet.Cmd.data.ContactSource))
            {
                _logger.LogDebug("Skipping contact history insertion due to invalid source: {Source}", packet.Cmd.data.ContactSource);
                return;
            }

            // Fetch the related loan account using the account ID
            _loanAccount = await _loanAccountRepository.GetLoanAccountsByIdAsync(packet.Cmd.data.AccountId, _flexAppContext);
            if (_loanAccount == null)
            {
                _logger.LogWarning("Loan account not found for AccountId: {AccountId}", packet.Cmd.data.AccountId);
                return;
            }


            // Try adding each type of contact if it exists in the request
            await InsertAccountContactHistory(packet);

        }

        // Tries to add contact history if not already present
        private async Task InsertAccountContactHistory( AddContactHistoryPostBusDataPacket packet)
        {
            ContactSourceEnum contactSourceName = (ContactSourceEnum)Enum.Parse(typeof(ContactSourceEnum), packet.Cmd.data.ContactSource);
            string accountId = packet.Cmd.data.AccountId;

            // Map contact types to values
            var contacts = new Dictionary<ContactTypeEnum, string?>
              {
                  { ContactTypeEnum.Mobile, packet.Cmd.data.MobileNo },
                  { ContactTypeEnum.Email, packet.Cmd.data.EmailId },
                  { ContactTypeEnum.Address, packet.Cmd.data.Address }
              };

            foreach (var entry in contacts)
            {
                var contactType = entry.Key;
                var contactValue = entry.Value;

                if (string.IsNullOrWhiteSpace(contactValue))
                    continue;

                var exists =await _loanAccountContactHistoryQueryRepository.GetAccountContactHistoryExistsAsync(contactSourceName, contactType, contactValue, accountId, _flexAppContext);

                if (!exists)
                {
                    var dto = packet.Cmd.data;

                    var contactHistory = new AccountContactHistory(
                        contactValue,
                        dto.Latitude,
                        dto.Longitude,
                         contactSourceName,
                        contactType,
                        accountId,
                        _flexAppContext?.UserId ?? string.Empty
                    );

                    _loanAccount!.AddAccountContactHistory(contactHistory);
                }
            }

            await _loanAccountRepository.SaveAsync(_flexAppContext, _loanAccount);
        }
    }
}
