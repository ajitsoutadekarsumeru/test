using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetCollections : FlexiQueryEnumerableBridgeAsync<Collection, GetCollectionsDto>
    {
        protected readonly ILogger<GetCollections> _logger;
        protected GetCollectionsParams _params;
        protected readonly IRepoFactory _repoFactory;
        private CollectionWorkflowState state;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetCollections(ILogger<GetCollections> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetCollections AssignParameters(GetCollectionsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetCollectionsDto>> Fetch()
        {
            state = new Cancelled();
            var result = await Build<Collection>().Where(c => c.CollectionWorkflowState.Name != state.Name).SelectTo<GetCollectionsDto>().ToListAsync();

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
                                    .CollectionDateRange(_params.FromDate, _params.ToDate)
                                    .ByCollectionAccountNo(_params.AgreementId)
                                    .ByCollector(_params.CollectorId)
                                    .ByCustomerName(_params.CustomerName)
                                    .IncludeCollectionWorkflowState()
                                    .IncludeCollector()
                                    .IncludeAccount(); ;

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetCollectionsParams : DtoBridge
    {
        [Required]
        public string? AgreementId { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid CustomerName")]
        public string? CustomerName { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid CollectorId")]
        public string? CollectorId { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid ReceiptNo")]
        public string? ReceiptNo { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }
    }
}