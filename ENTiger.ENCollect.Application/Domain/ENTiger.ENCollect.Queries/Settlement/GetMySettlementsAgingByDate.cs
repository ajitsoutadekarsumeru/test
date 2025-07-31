
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMySettlementsAgingByDate : FlexiQueryEnumerableBridgeAsync<Settlement, GetMySettlementsAgingByDateDto>
    {
        protected readonly ILogger<GetMySettlementsAgingByDate> _logger;
        protected GetMySettlementsAgingByDateParams _params;
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
        public GetMySettlementsAgingByDate(ILogger<GetMySettlementsAgingByDate> logger, RepoFactory repoFactory, MySettlementsService mySettlementsService)
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
        public virtual GetMySettlementsAgingByDate AssignParameters(GetMySettlementsAgingByDateParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetMySettlementsAgingByDateDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            var settlements = await Build<Settlement>().ToListAsync();

            var result = await _mySettlementsService.GetMySettlementsAgingByDateSummaryAsync(_flexAppContext, settlements);

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
    public class GetMySettlementsAgingByDateParams : DtoBridge
    {
       
    }
}
