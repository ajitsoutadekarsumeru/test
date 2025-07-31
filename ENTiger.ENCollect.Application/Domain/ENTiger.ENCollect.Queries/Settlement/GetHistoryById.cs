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
    public class GetHistoryById : FlexiQueryBridgeAsync<Settlement, GetHistoryByIdDto>
    {
        protected readonly ILogger<GetHistoryById> _logger;
        protected GetHistoryByIdParams _params;
        protected readonly RepoFactory _repoFactory;


        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _repoSettlement;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetHistoryById(ILogger<GetHistoryById> logger, RepoFactory repoFactory, ISettlementRepository settlementRepository, IMapper mapper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _repoSettlement = settlementRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetHistoryById AssignParameters(GetHistoryByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<GetHistoryByIdDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            var result = Build<Settlement>()
                             .Select(s => new GetHistoryByIdDto
                             {
                                 WorkflowHistory = s.StatusHistory.Select(h => new WorkFlowHistoryDto
                                 {
                                     StatusUpdatedDate = h.ChangedDate.UtcDateTime,
                                     Status = h.ToStatus,
                                     Action = h.Action,
                                     Remarks = h.Comment,
                                     RejectionReason = h.Comment,
                                     EmployeeId = h.ChangedByUserId
                                 }).ToList()
                             })
                            .FirstOrDefault();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                .Include(i => i.StatusHistory)
                                            .Where(w => w.Id == _params.Id);

            return query;

            //return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetHistoryByIdParams : DtoBridge
    {
        public string Id { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }
}