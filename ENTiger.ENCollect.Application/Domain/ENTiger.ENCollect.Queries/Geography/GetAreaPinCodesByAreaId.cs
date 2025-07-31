using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetAreaPinCodesByAreaId : FlexiQueryEnumerableBridgeAsync<AreaPinCodeMapping, GetAreaPinCodesByAreaIdDto>
    {
        protected readonly ILogger<GetAreaPinCodesByAreaId> _logger;
        protected GetAreaPinCodesByAreaIdParams _params;
        protected readonly IRepoFactory _repoFactory;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="repoFlex"></param>
        /// <param name="connectionProvider"></param>
        public GetAreaPinCodesByAreaId(ILogger<GetAreaPinCodesByAreaId> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetAreaPinCodesByAreaId AssignParameters(GetAreaPinCodesByAreaIdParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetAreaPinCodesByAreaIdDto>> Fetch()
        {
            var result = await Build<AreaPinCodeMapping>().SelectTo<GetAreaPinCodesByAreaIdDto>().ToListAsync();

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
                                      .ByAreaPinCodeMapping(_params.Id)
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
    public class GetAreaPinCodesByAreaIdParams : DtoBridge
    {
        public string Id { get; set; }
    }
}