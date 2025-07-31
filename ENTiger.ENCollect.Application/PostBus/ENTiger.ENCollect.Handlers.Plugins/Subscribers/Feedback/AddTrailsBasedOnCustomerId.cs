using ENCollect.Security;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddTrailsBasedOnCustomerId : IAddTrails
    {
        private readonly ILogger<AddTrailsBasedOnCustomerId> _logger;
        private readonly RepoFactory _repoFactory;
        private FlexAppContextBridge? _flexAppContext;
        protected readonly IFlexHost _flexHost;
        private readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        private readonly EncryptionSettings _encryptionSettings;
        private readonly IFlexServiceBusBridge _serviceBus;
        private readonly SystemUserSettings _autoTrailSettings;
        private readonly AesGcmCrypto _aesGcmCrypto;

        public AddTrailsBasedOnCustomerId(ILogger<AddTrailsBasedOnCustomerId> logger, IFlexHost flexHost, RepoFactory repoFactory,
            IFlexPrimaryKeyGeneratorBridge pkGenerator, IOptions<EncryptionSettings> encryptionSettings, IFlexServiceBusBridge servicebus,
            IOptions<SystemUserSettings> autoTrailSettings, AesGcmCrypto aesGcmCrypto)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _pkGenerator = pkGenerator;
            _encryptionSettings = encryptionSettings.Value;
            _autoTrailSettings = autoTrailSettings.Value;
            _serviceBus = servicebus;
            _aesGcmCrypto = aesGcmCrypto;
        }

        public virtual async Task Execute(FeedbackAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; // Do not remove this line
            _repoFactory.Init(@event);

            // Get feedback details
            var feedback = await _repoFactory.GetRepo().FindAll<Feedback>()
                .FlexInclude(x => x.Account)
                .Where(feedback => feedback.Id == @event.Id).FirstOrDefaultAsync();

            if (feedback == null || feedback.TransactionSource == TransactionSourceEnum.System.Value) return; //do no recapture the same auto trails

            // Check whether dispositioncode belongs to Account Level or Customer Level
            var dispositioncode = await _repoFactory.GetRepo().FindAll<DispositionCodeMaster>()
                                                //TODO: foreign key relationship possible here?
                                                .Where(a => a.DispositionCode == feedback.DispositionCode).FirstOrDefaultAsync();

            if (dispositioncode != null && dispositioncode.DispositionCodeIsCustomerLevel && !string.IsNullOrWhiteSpace(feedback.Account.CUSTOMERID))
            {
                // Fetch all linked accounts of customerId
                // exclude null account numbers and first account where feedback was loaded
                //TODO: make agreementid not nullable on the model
                List<string> accountlist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .GetLinkedAccountsByCustomerId(feedback.Account.CUSTOMERID, feedback.AccountId)
                                                .Select(a => a.AGREEMENTID).ToListAsync();

                if (accountlist.Count() > 0)
                {
                    await CreateAutoTrailsAsync(feedback, dispositioncode, accountlist);
                }
            }
        }

        private async Task CreateAutoTrailsAsync(Feedback feedback, DispositionCodeMaster dispositionCodeMaster, List<string> linkedAccounts)
        {
            string key = _encryptionSettings.StaticKeys.DecryptionKey;

            _flexAppContext.UserId = _autoTrailSettings.SystemUserId;
            _logger.LogInformation("AddTrailsBasedOnCustomerId : Start  UserId : " + _flexAppContext?.UserId);
            _flexAppContext.RequestSource = TransactionSourceEnum.System.Value; //_model.TransactionSource = _flexAppContext?.RequestSource; -> used in AddFeedbackPlugin
            var aesGcmKey = Encoding.UTF8.GetBytes(key);

            AddFeedbackDto dto = _flexHost.Convert<Feedback, AddFeedbackDto>(feedback);

            foreach (var acc in linkedAccounts)
            {
                dto.Accountno = _aesGcmCrypto.Encrypt(acc, aesGcmKey);
                dto.SetGeneratedId(_pkGenerator.GenerateKey());
                dto.SetAppContext(_flexAppContext);
                var command = new AddFeedbackCommand { Dto = dto };
                await _serviceBus.Send(command);
            }
        }
    }
}
