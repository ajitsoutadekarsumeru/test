
namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMyClosedSettlements : FlexiQueryEnumerableBridgeAsync<GetMyClosedSettlementsDto>
    {
        
        protected readonly ILogger<GetMyClosedSettlements> _logger;
        protected GetMyClosedSettlementsParams _params;
        protected readonly RepoFactory _repoFactory;

        private readonly MySettlementsService _mySettlementsService;
        protected FlexAppContextBridge? _flexAppContext;
        protected string id = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMyClosedSettlements(ILogger<GetMyClosedSettlements> logger, RepoFactory repoFactory, MySettlementsService mySettlementsService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _mySettlementsService = mySettlementsService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMyClosedSettlements AssignParameters(GetMyClosedSettlementsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMyClosedSettlementsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

            var result = await _mySettlementsService.GetMyCloseSettlementSummaryAsync(_flexAppContext, id);

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMyClosedSettlementsParams : DtoBridge
    {

    }
}
