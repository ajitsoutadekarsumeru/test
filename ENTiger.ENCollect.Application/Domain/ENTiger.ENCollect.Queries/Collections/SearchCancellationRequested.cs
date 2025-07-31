using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchCancellationRequested : FlexiQueryPagedListBridgeAsync<Collection, SearchCancellationRequestedParams, SearchCancellationRequestedDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchCancellationRequested> _logger;
        protected SearchCancellationRequestedParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchCancellationRequested(ILogger<SearchCancellationRequested> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchCancellationRequested AssignParameters(SearchCancellationRequestedParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<FlexiPagedList<SearchCancellationRequestedDto>> Fetch()
        {
            var projection = await Build<Collection>().SelectTo<SearchCancellationRequestedDto>().ToListAsync();

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
            CollectionWorkflowState state = new CancellationRequested();

            IQueryable<T> query = _repoFactory.GetRepo().FindAll<T>()
                                    .ByCollectionWorkflowState(state)
                                    .CollectionDateRange(_params.ReceiptFromDate, _params.ReceiptToDate)
                                    .ByCollectionAgencyCode(_params.AgencyId)
                                    .ByCollectionAccountNo(_params.AgreementId); ;

            //Build Your Query With All Parameters Here

            query = CreatePagedQuery<T>(query, _params.PageNumber, _params.skip, _params.take);

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchCancellationRequestedParams : PagedQueryParamsDtoBridge
    {
        public string? AgreementId { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid AgencyId")]
        public string? AgencyId { get; set; }

        public DateTime? ReceiptFromDate { get; set; }
        public DateTime? ReceiptToDate { get; set; }
        public int take { get; set; }

        public int skip { get; set; }
    }
}