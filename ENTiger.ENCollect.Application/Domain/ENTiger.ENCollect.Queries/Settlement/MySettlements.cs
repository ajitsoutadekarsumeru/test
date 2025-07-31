using System.Data;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public class MySettlements : FlexiQueryBridgeAsync<Settlement, MySettlementsSummaryDto>
    {
        protected readonly ILogger<MySettlements> _logger;
        protected MySettlementsSummaryParams _params;
        protected readonly IRepoFactory _repoFactory;
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
        public MySettlements(ILogger<MySettlements> logger, 
            IRepoFactory repoFactory,
            MySettlementsService mySettlementsService
            )
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
        public virtual MySettlements AssignParameters(MySettlementsSummaryParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<MySettlementsSummaryDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;
           
            MySettlementsSummaryDto result = await _mySettlementsService.GetMySettlementsSummaryAsync(_flexAppContext, id);
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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(x => x.Id == id);

            //Build Your Query Here

            return query;
        }

      
    }

    /// <summary>
    ///
    /// </summary>
    public class MySettlementsSummaryParams : DtoBridge
    {
    }
}