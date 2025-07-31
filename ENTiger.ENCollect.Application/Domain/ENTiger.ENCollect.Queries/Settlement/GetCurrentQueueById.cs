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
    public class GetCurrentQueueById : FlexiQueryPagedListBridge<SettlementQueueProjection, GetCurrentQueueByIdParams, GetCurrentQueueByIdDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetCurrentQueueById> _logger;
        protected GetCurrentQueueByIdParams _params;
        protected readonly RepoFactory _repoFactory;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCurrentQueueById(ILogger<GetCurrentQueueById> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCurrentQueueById AssignParameters(GetCurrentQueueByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override FlexiPagedList<GetCurrentQueueByIdDto> Fetch()
        {
            var projection = Build<SettlementQueueProjection>().SelectTo<GetCurrentQueueByIdDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                .Include(s => s.Settlement)
                .Where(x => x.SettlementId == _params.Id && x.IsDeleted == false);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.Skip, _params.Take);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetCurrentQueueByIdParams : PagedQueryParamsDtoBridge
    {
        public string Id { get; set; }
        public int Take { get; set; }
    }
}
