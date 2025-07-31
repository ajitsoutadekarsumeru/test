using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMySettlementDetailsByAging : FlexiQueryPagedListBridgeAsync<Settlement, GetMySettlementDetailsByAgingParams, GetMySettlementDetailsByAgingDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetMySettlementDetailsByAging> _logger;
        protected GetMySettlementDetailsByAgingParams _params;
        protected readonly RepoFactory _repoFactory;

        protected FlexAppContextBridge? _flexAppContext;
        protected string id = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetMySettlementDetailsByAging(ILogger<GetMySettlementDetailsByAging> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetMySettlementDetailsByAging AssignParameters(GetMySettlementDetailsByAgingParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<GetMySettlementDetailsByAgingDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

            var now = DateTime.Now;
            bool useCreatedDate = _params.AgingType == "date";

            var settlements = await Build<Settlement>().ToListAsync();

            var projection = settlements.Where(w =>
                                        (now - (useCreatedDate ? w.CreatedDate : w.StatusUpdatedOn)).Days >= _params.FromDay
                                        && (_params.ToDay == null || (now - (useCreatedDate ? w.CreatedDate : w.StatusUpdatedOn)).Days <= _params.ToDay.Value)
                                    ).Select(x => new GetMySettlementDetailsByAgingDto
                                    {
                                        Id = x.Id,
                                        CustomId = x.CustomId,
                                        AccountId = x.LoanAccountId,
                                        TOS = x.TOS,
                                        Status = x.Status,
                                        SettlementAmount = x.SettlementAmount,
                                        RenegotiationAmount = x.RenegotiationAmount,
                                        RejectionReason = x.SettlementRemarks,
                                        StatusChangedDate = x.StatusUpdatedOn,
                                        CreatedDate = x.CreatedDate
                                    }).ToList();

            var result = BuildPagedOutput(projection);
            foreach (var item in result)
            {
                var since = item?.StatusChangedDate ?? item.CreatedDate;
                var timeSpan = now - since;
                var agingDays = (int)timeSpan.TotalDays;

                item.AgingInCurrentStatusDays = agingDays;
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

            var settlementOpenStatus = SettlementStatusEnum.ByGroup("Open").Select(s => s.Value).ToHashSet();

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                        .ByCreatedBy(id)
                                        .ByStatus(settlementOpenStatus)
                                        .OrderByDescending(s=>s.CreatedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);
            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetMySettlementDetailsByAgingParams : PagedQueryParamsDtoBridge
    {
        public int FromDay { get; set; }
        public int? ToDay { get; set; }
        public string AgingType { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}
