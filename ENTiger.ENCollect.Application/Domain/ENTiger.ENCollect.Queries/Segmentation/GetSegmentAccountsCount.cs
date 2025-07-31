using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSegmentAccountsCount : FlexiQueryBridgeAsync<GetSegmentAccountsCountDto>
    {
        protected readonly ILogger<GetSegmentAccountsCount> _logger;
        protected GetSegmentAccountsCountParams _params;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetSegmentAccountsCount(ILogger<GetSegmentAccountsCount> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetSegmentAccountsCount AssignParameters(GetSegmentAccountsCountParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetSegmentAccountsCountDto> Fetch()
        {
            GetSegmentAccountsCountDto result = null;

            return result;
        }
    }

    public class GetSegmentAccountsCountParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Id { get; set; }
    }
}