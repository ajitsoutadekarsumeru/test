
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMySettlementsAgingByStatus : FlexiQueryEnumerableBridgeAsync<Settlement, GetMySettlementsAgingByStatusDto>
    {
        protected readonly ILogger<GetMySettlementsAgingByStatus> _logger;
        protected GetMySettlementsAgingByStatusParams _params;
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
        public GetMySettlementsAgingByStatus(ILogger<GetMySettlementsAgingByStatus> logger, RepoFactory repoFactory, MySettlementsService mySettlementsService)
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
        public virtual GetMySettlementsAgingByStatus AssignParameters(GetMySettlementsAgingByStatusParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMySettlementsAgingByStatusDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;


            var settlements = await Build<Settlement>().ToListAsync();

            var result = await _mySettlementsService.GetMySettlementsAgingByStatusSummaryAsync(_flexAppContext, settlements);



            return result;
        }
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            var settlementOpenStatus = SettlementStatusEnum.ByGroup("Open").Select(s => s.Value).ToHashSet();
            var userId = _flexAppContext?.UserId ?? string.Empty;
            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByCreatedBy(userId)
                                        .ByStatus(settlementOpenStatus);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMySettlementsAgingByStatusParams : DtoBridge
    {
       
    }
}
