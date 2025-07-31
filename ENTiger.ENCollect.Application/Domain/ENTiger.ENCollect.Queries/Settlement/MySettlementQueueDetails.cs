using System.ComponentModel.DataAnnotations;
using System.Data;
using Elastic.Clients.Elasticsearch.Snapshot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    ///
    /// </summary>
    public class MySettlementQueueDetails : FlexiQueryPagedListBridgeAsync<SettlementQueueProjection, MySettlementQueueDetailsParams, MySettlementQueueDetailsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MySettlementQueueDetails> _logger;
        protected MySettlementQueueDetailsParams _params;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        private readonly MySettlementsQueueService _mySettlementsQueueService;

        protected string id = string.Empty;
        protected string _status = string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public MySettlementQueueDetails(ILogger<MySettlementQueueDetails> logger,
            IRepoFactory repoFactory, MySettlementsQueueService mySettlementsQueueService
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _mySettlementsQueueService = mySettlementsQueueService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual MySettlementQueueDetails AssignParameters(MySettlementQueueDetailsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<MySettlementQueueDetailsDto>> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            var settlementQueue = await Build<SettlementQueueProjection>().ToListAsync();

            var projection = await _mySettlementsQueueService.GetMySettlementQueueDetailsAsync(_flexAppContext, id, _params.Status, settlementQueue);
            var result = BuildPagedOutput(projection);

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

            _status = SettlementStatusEnum.ByDisplayName(_params.Status).Value;
            var userId = _flexAppContext?.UserId ?? string.Empty;

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .AsNoTracking()
                                    .FlexInclude(s => s.Settlement)
                                    .FlexInclude(s => s.Settlement.StatusHistory)
                                    .FlexInclude(s => s.Settlement.WaiverDetails)
                                    .Where(s => s.ApplicationUserId == userId 
                                    && s.IsDeleted == false
                                    && s.Settlement.Status == _status)
                                    .OrderByDescending(s => s.CreatedDate);

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);
            return query;
        }


    }

    /// <summary>
    ///
    /// </summary>
    public class MySettlementQueueDetailsParams : PagedQueryParamsDtoBridge
    {
        [Required]
        public string Status { get; set; }
       

        public int take { get; set; }
        public int skip { get; set; }
    }
}