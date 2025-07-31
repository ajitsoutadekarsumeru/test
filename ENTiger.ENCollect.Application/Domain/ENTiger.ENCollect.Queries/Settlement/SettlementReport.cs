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
    public class SettlementReport : FlexiQueryPagedListBridgeAsync<Settlement, SettlementReportParams, SettlementReportDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SettlementReport> _logger;
        protected SettlementReportParams _params;
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
        public SettlementReport(ILogger<SettlementReport> logger, IRepoFactory repoFactory
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
        public virtual SettlementReport AssignParameters(SettlementReportParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SettlementReportDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            id = _flexAppContext.UserId;

          
            var projection = Build<Settlement>().SelectTo<SettlementReportDto>().ToList();

            var result = BuildPagedOutput(projection);

            var now = DateTime.Now;
            foreach (var item in result)
            {
                var sinceStatusUpdated = item.StatusUpdatedOn.Date;
                var sinceCreated = item.CreatedDate.Date;
                
                var timeSpan = now - sinceStatusUpdated;
                var agingSinceStatusUpdatedDays = (int)timeSpan.TotalDays;

                var timeSpan2 = now - sinceCreated;
                var agingSinceRequestedDays = (int)timeSpan.TotalDays;

                item.AgingInCurrentStatusDays = agingSinceStatusUpdatedDays;
                item.AgingSinceRequestedDays = agingSinceRequestedDays;
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
                .ByStatus(_params.Status)
                .ByCustomId(_params.settlementRequestId)
                .ByCreatedBy(id)
                .ByCreatedDateRange(_params.CreatedFrom, _params.CreatedTo)
                .ByAgingSinceCreated(_params.AgingSinceRequestedFrom, _params.AgingSinceRequestedTo)
                .ByAgingSinceUpdated(_params.AgingSinceStatusUpdatedFrom, _params.AgingSinceStatusUpdatedTo);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }


    }

    /// <summary>
    ///
    /// </summary>
    public class SettlementReportParams : PagedQueryParamsDtoBridge
    {
        public string? settlementRequestId { get; set; }
        public string? Branch { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public int? AgingSinceStatusUpdatedFrom { get; set; }
        public int? AgingSinceStatusUpdatedTo { get; set; }
        public int? AgingSinceRequestedFrom { get; set; }
        public int? AgingSinceRequestedTo { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }
    }
}