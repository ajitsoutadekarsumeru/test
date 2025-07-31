using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAreaPinCodes : FlexiQueryEnumerableBridgeAsync<AreaPinCodeMapping, SearchAreaPinCodesDto>
    {
        protected readonly ILogger<SearchAreaPinCodes> _logger;
        protected SearchAreaPinCodesParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public SearchAreaPinCodes(ILogger<SearchAreaPinCodes> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual SearchAreaPinCodes AssignParameters(SearchAreaPinCodesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<SearchAreaPinCodesDto>> Fetch()
        {
            var result = await Build<AreaPinCodeMapping>().SelectTo<SearchAreaPinCodesDto>().ToListAsync();

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
                                      .ByPinCodeAreaSearch(_params.query)
                                      .ByDeleteAreaPinCode()
                                      .FlexInclude(x => x.Area)
                                      .FlexInclude(x => x.PinCode);

            //Build Your Query Here

            return query;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SearchAreaPinCodesParams : DtoBridge
    {
        public string query { get; set; }
    }
}