using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetSegmentAccounts : FlexiQueryBridgeAsync<GetSegmentAccountsDto>
    {
        protected readonly ILogger<GetSegmentAccounts> _logger;
        protected GetSegmentAccountsParams _params;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetSegmentAccounts(ILogger<GetSegmentAccounts> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetSegmentAccounts AssignParameters(GetSegmentAccountsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetSegmentAccountsDto> Fetch()
        {
            GetSegmentAccountsDto result = null;

            return result;
        }
    }

    public class GetSegmentAccountsParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string Id { get; set; }
    }
}