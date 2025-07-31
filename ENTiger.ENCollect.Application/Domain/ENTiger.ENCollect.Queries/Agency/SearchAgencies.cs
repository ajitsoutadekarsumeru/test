using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAgencies : FlexiQueryPagedListBridgeAsync<Agency, SearchAgenciesParams, SearchAgenciesDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchAgencies> _logger;
        protected SearchAgenciesParams _params;
        protected readonly IRepoFactory _repoFactory;
        private AgencyWorkflowState workflowState;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchAgencies(ILogger<SearchAgencies> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchAgencies AssignParameters(SearchAgenciesParams @params)
        {
            _params = @params;
            workflowState = WorkflowStateFactory.GetCollectionAgencyWFState(_params.Status);
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchAgenciesDto>> Fetch()
        {
            var projection = await Build<Agency>().SelectTo<SearchAgenciesDto>().ToListAsync();

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
                                    .FlexInclude(a => a.AgencyWorkflowState)
                                    .ByAgencyWorkFlowState(workflowState)
                                    .ByAgencyName(_params.AgencyName)
                                    .ByExpiryDate(_params.ExpiryDate)
                                    .ByDeferredDate(_params.DeferredDate)
                                    .OrderByDescending(x => x.CreatedDate);

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAgenciesParams : PagedQueryParamsDtoBridge
    {
        public string AgencyName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        public DateTime? DeferredDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public int take { get; set; }

        public int skip { get; set; }
    }
}