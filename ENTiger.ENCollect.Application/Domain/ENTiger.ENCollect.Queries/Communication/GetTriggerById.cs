using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg;
using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTriggerById : FlexiQueryBridge<CommunicationTrigger, GetTriggerByIdDto>
    {
        protected readonly ILogger<GetTriggerById> _logger;
        protected GetTriggerByIdParams _params;
        protected readonly RepoFactory _repoFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetTriggerById(ILogger<GetTriggerById> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetTriggerById AssignParameters(GetTriggerByIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override GetTriggerByIdDto Fetch()
        {
            var result = Build<CommunicationTrigger>().SelectTo<GetTriggerByIdDto>().FirstOrDefault();

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

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>().ByTriggersId(_params.Id);
            return query;
        }
    }

    public class GetTriggerByIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}
