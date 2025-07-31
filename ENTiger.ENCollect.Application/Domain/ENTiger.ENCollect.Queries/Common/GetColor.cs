using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetColor : FlexiQueryBridgeAsync<GetColorDto>
    {
        protected readonly ILogger<GetColor> _logger;
        protected GetColorParams _params;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetColor(ILogger<GetColor> logger, IFlexTenantRepository<FlexTenantBridge> repoTenantFactory)
        {
            _logger = logger;
            _repoTenantFactory = repoTenantFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual GetColor AssignParameters(GetColorParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<GetColorDto> Fetch()
        {
            _flexAppContext = _params.GetAppContext();
            string tenantId = _flexAppContext.TenantId;

            GetColorDto result = null;

            result = await _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId).Select(x => new GetColorDto { Color = x.Color })?.FirstOrDefaultAsync();

            return result;
        }
    }

    public class GetColorParams : DtoBridge
    {
        //Change the below Id field name/type according to your domain
        public string? Color { get; set; }
    }
}