using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchTriggers : FlexiQueryPagedListBridgeAsync<CommunicationTrigger, SearchTriggersParams, SearchTriggersDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchTriggers> _logger;
        protected SearchTriggersParams _params;
        protected readonly IRepoFactory _repoFactory;
        /// <summary>
        private int _skip;
        private int _take;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchTriggers(ILogger<SearchTriggers> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchTriggers AssignParameters(SearchTriggersParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchTriggersDto>> Fetch()
        {
            var projection = await Build<CommunicationTrigger>().SelectTo<SearchTriggersDto>().ToListAsync();

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
                                     .ExcludeDeletedTrigger()
                                     .ByTriggersName(_params.TriggerName)
                                     .ByTriggerStatus(_params.IsActive)
                                     .OrderByDescending(a => a.LastModifiedDate);

            //Build Your Query Here
            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchTriggersParams : PagedQueryParamsDtoBridge
    {
        [RegularExpression("^[a-zA-Z0-9_ ]*$", ErrorMessage = "Invalid Name")]
        public string? TriggerName { get; set; }
        public bool? IsActive { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
