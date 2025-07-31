using ENCollect.Security;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCreditAccounts : FlexiQueryEnumerableBridgeAsync<LoanAccount, SearchCreditAccountsDto>
    {
        protected readonly ILogger<SearchCreditAccounts> _logger;
        protected SearchCreditAccountsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected string _userId;
        private readonly EncryptionSettings _encryptionSettings;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCreditAccounts(ILogger<SearchCreditAccounts> logger, IRepoFactory repoFactory
            , IOptions<EncryptionSettings> encryptionSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _encryptionSettings = encryptionSettings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCreditAccounts AssignParameters(SearchCreditAccountsParams @params)
        {
            _params = @params;
            _userId = _params.GetAppContext()?.UserId;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchCreditAccountsDto>> Fetch()
        {
            var aesGcmCrypto = new AesGcmCrypto();

            string key = _encryptionSettings.StaticKeys.DecryptionKey;
            var aesGcmKey = Encoding.UTF8.GetBytes(key);

            _params.CreditCardNumber = aesGcmCrypto.Decrypt(_params.CreditCardNumber, aesGcmKey);

            var result = await Build<LoanAccount>().SelectTo<SearchCreditAccountsDto>().ToListAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCreditCardNumber(_params.CreditCardNumber)
                                    .ByMobileNumber(_params.MobileNumber);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCreditAccountsParams : DtoBridge
    {
        public string? CreditCardNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? ReferenceId { get; set; }
    }
}