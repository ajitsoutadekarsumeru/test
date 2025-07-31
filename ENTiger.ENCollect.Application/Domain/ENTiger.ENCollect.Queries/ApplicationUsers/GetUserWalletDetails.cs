using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserWalletDetails : FlexiQueryBridgeAsync<ApplicationUser, GetUserWalletDetailsDto>
    {
        protected readonly ILogger<GetUserWalletDetails> _logger;
        protected GetUserWalletDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IWalletRepository _walletRepository;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly WalletSettings _walletSettings;
        private string _userId = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetUserWalletDetails(ILogger<GetUserWalletDetails> logger, 
            IRepoFactory repoFactory,
            IWalletRepository walletRepository,
            IOptions<WalletSettings> walletSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _walletRepository = walletRepository;
            _walletSettings = walletSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetUserWalletDetails AssignParameters(GetUserWalletDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// Fetches the user's wallet details.
        /// </summary>
        public override async Task<GetUserWalletDetailsDto> Fetch()
        {
            _flexAppContext = _params?.GetAppContext();  // Do not remove this line
            _userId = _flexAppContext?.UserId ?? string.Empty;

            if (string.IsNullOrEmpty(_userId))
            {
                _logger.LogWarning("User ID is null or empty while fetching wallet details.");
                return new GetUserWalletDetailsDto(); // Return empty object to prevent issues
            }

            var result = await Build<ApplicationUser>().SelectTo<GetUserWalletDetailsDto>().FirstOrDefaultAsync()
                         ?? new GetUserWalletDetailsDto();

            if (result.WalletLimit > 0) 
            {               
                result.WalletUsedColor = await _walletRepository.GetUtilizationColor(result.WalletUsedPercentage);
            }
            

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
                .FlexInclude(a=>a.Wallet)
                .Where(x => x.Id == _userId);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetUserWalletDetailsParams : DtoBridge
    {

    }
}
