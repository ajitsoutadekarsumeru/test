using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public class MySettlementQueue : FlexiQueryEnumerableBridgeAsync<Settlement, CaseGroupSummaryDto>
    {
        protected readonly ILogger<MySettlementQueue> _logger;
        protected MySettlementQueueParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        protected string id = string.Empty;


        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MySettlementQueue(ILogger<MySettlementQueue> logger, 
            IRepoFactory repoFactory, ISettlementRepository settlementRepository
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MySettlementQueue AssignParameters(MySettlementQueueParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<CaseGroupSummaryDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

            var settlements = await _settlementRepository.GetSettlementsAssignedToAsync(_flexAppContext,id);
            return settlements
                .GroupBy(s => s.Status)
                .Select(g => new CaseGroupSummaryDto
                {
                    Status = SettlementStatusEnum.ByValue(g.Key).DisplayName,
                    Count = g.Count(),
                    TotalAmount = g.Sum(s => s.SettlementAmount)
                })
                .Append(new CaseGroupSummaryDto
                {
                    Status = "Total",
                    Count = settlements.Count(),
                    TotalAmount = settlements.Sum(s => s.SettlementAmount)
                })
                .ToList();
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
    public class MySettlementQueueParams : DtoBridge
    {
    }
}