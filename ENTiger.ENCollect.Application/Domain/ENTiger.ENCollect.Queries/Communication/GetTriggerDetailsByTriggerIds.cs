using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTriggerDetailsByTriggerIds : FlexiQueryEnumerableBridge<CommunicationTrigger, GetTriggerDetailsByTriggerIdsDto>
    {
        
        protected readonly ILogger<GetTriggerDetailsByTriggerIds> _logger;
        protected GetTriggerDetailsByTriggerIdsParams _params;
        protected readonly RepoFactory _repoFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetTriggerDetailsByTriggerIds(ILogger<GetTriggerDetailsByTriggerIds> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTriggerDetailsByTriggerIds AssignParameters(GetTriggerDetailsByTriggerIdsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<GetTriggerDetailsByTriggerIdsDto> Fetch()
        {
            var result = Build<CommunicationTrigger>().SelectTo<GetTriggerDetailsByTriggerIdsDto>().ToList();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByTriggerIds(_params.TriggerIds);
            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GetTriggerDetailsByTriggerIdsParams : DtoBridge
    {
        public List<string> TriggerIds { get; set; }
    }
}
