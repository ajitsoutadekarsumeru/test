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
    public class GetInstallmentsById : FlexiQueryBridgeAsync<Settlement, GetInstallmentsByIdDto>
    {
        protected readonly ILogger<GetInstallmentsById> _logger;
        protected GetInstallmentsByIdParams _params;
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
        public GetInstallmentsById(ILogger<GetInstallmentsById> logger, RepoFactory repoFactory
                , ISettlementRepository settlementRepository, IMapper mapper)
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
        public virtual GetInstallmentsById AssignParameters(GetInstallmentsByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<GetInstallmentsByIdDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            var installments = await _repoSettlement.GetMySettlementsInstallmentsBySettlementIdAsync(_flexAppContext, _params.Id);

            return _mapper.Map<GetInstallmentsByIdDto>(installments);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected override IQueryable<T> Build<T>()
        {
            _repoFactory.Init(_params);

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>();

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetInstallmentsByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}
