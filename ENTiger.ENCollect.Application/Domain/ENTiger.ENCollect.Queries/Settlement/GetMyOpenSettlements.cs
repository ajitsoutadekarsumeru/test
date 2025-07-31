namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMyOpenSettlements : FlexiQueryEnumerableBridgeAsync<GetMyOpenSettlementsDto>
    {
        
        protected readonly ILogger<GetMyOpenSettlements> _logger;
        protected GetMyOpenSettlementsParams _params;
        protected readonly RepoFactory _repoFactory;

        protected FlexAppContextBridge? _flexAppContext;
        protected string id = string.Empty;
        private readonly MySettlementsService _mySettlementsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMyOpenSettlements(ILogger<GetMyOpenSettlements> logger, RepoFactory repoFactory, MySettlementsService mySettlementsService)
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
        public virtual GetMyOpenSettlements AssignParameters(GetMyOpenSettlementsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMyOpenSettlementsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

            var result = await _mySettlementsService.GetMyOpenSettlementSummaryAsync(_flexAppContext, id);

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMyOpenSettlementsParams : DtoBridge
    {

    }
}
