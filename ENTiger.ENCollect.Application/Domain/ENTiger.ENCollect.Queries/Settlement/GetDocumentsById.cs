using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDocumentsById : FlexiQueryBridgeAsync<Settlement, GetDocumentsByIdDto>
    {
        protected readonly ILogger<GetDocumentsById> _logger;
        protected GetDocumentsByIdParams _params;
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
        public GetDocumentsById(ILogger<GetDocumentsById> logger, RepoFactory repoFactory
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
        public virtual GetDocumentsById AssignParameters(GetDocumentsByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<GetDocumentsByIdDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            var documents = await _repoSettlement.GetMySettlementsDocumentsBySettlementIdAsync(_flexAppContext, _params.Id);

            return _mapper.Map<GetDocumentsByIdDto>(documents);
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
    public class GetDocumentsByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}
