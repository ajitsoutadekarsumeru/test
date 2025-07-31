using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Linq;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWaiversById : FlexiQueryBridgeAsync<Settlement, GetWaiversByIdDto>
    {
        
        protected readonly ILogger<GetWaiversById> _logger;
        protected GetWaiversByIdParams _params;
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
        public GetWaiversById(ILogger<GetWaiversById> logger, RepoFactory repoFactory
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
        public virtual GetWaiversById AssignParameters(GetWaiversByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<GetWaiversByIdDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();

            IReadOnlyCollection<WaiverDetail> waiverDetails = await _repoSettlement.GetMySettlementsWaiversBySettlementIdAsync(_flexAppContext, _params.Id);

            GetWaiversByIdDto waiversByIdDto = _mapper.Map<GetWaiversByIdDto>(waiverDetails);
            return waiversByIdDto;
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

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetWaiversByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}
