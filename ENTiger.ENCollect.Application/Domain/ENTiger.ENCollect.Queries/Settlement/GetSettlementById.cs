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
    public class GetSettlementById : FlexiQueryBridge<Settlement, GetSettlementByIdDto>
    {
        protected readonly ILogger<GetSettlementById> _logger;
        protected GetSettlementByIdParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetSettlementById(ILogger<GetSettlementById> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetSettlementById AssignParameters(GetSettlementByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override GetSettlementByIdDto Fetch()
        {
            var result = Build<Settlement>().SelectTo<GetSettlementByIdDto>().FirstOrDefault();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().Where(t => t.Id == _params.Id);
            return query;
        }
    }

    public class GetSettlementByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}
