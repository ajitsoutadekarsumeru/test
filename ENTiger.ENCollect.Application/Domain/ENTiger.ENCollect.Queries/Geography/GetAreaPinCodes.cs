using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAreaPinCodes : FlexiQueryEnumerableBridgeAsync<AreaPinCodeMapping, GetAreaPinCodesDto>
    {
        protected readonly ILogger<GetAreaPinCodes> _logger;
        protected GetAreaPinCodesParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAreaPinCodes(ILogger<GetAreaPinCodes> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAreaPinCodes AssignParameters(GetAreaPinCodesParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAreaPinCodesDto>> Fetch()
        {
            var result = await Build<AreaPinCodeMapping>().SelectTo<GetAreaPinCodesDto>().ToListAsync();

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
    public class GetAreaPinCodesParams : DtoBridge
    {
    }
}