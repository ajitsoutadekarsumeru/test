using System.ComponentModel.DataAnnotations;
using System.Data;
using DocumentFormat.OpenXml.Spreadsheet;
using ENTiger.ENCollect.AccountsModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public class MySettlementSummaryDetails : FlexiQueryPagedListBridgeAsync<Settlement, MySettlementSummaryDetailsParams, MySettlementSummaryDetailsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MySettlementSummaryDetails> _logger;
        protected MySettlementSummaryDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected string id = string.Empty;
        protected string status = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MySettlementSummaryDetails(ILogger<MySettlementSummaryDetails> logger, IRepoFactory repoFactory
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MySettlementSummaryDetails AssignParameters(MySettlementSummaryDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<MySettlementSummaryDetailsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

            status = SettlementStatusEnum.ByDisplayName(_params.Status).Value;

            var projection = Build<Settlement>().SelectTo<MySettlementSummaryDetailsDto>().ToList();

            var result = BuildPagedOutput(projection);

            var now = DateTime.Now;
            foreach (var item in result)
            {
                var since = item?.StatusUpdatedOn ?? item.CreatedDate;
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

            IQueryable<T> query = _repoFactory.GetRepo()
                .FindAll<T>()
                .ByStatus(status)
                .ByCreatedBy(id)
                 .OrderByDescending(s => s.CreatedDate); 

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }


    }

    /// <summary>
    ///
    /// </summary>
    public class MySettlementSummaryDetailsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        public string Status { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}