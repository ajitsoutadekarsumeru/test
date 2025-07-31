using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public class GetTenants : FlexiQueryEnumerableBridgeAsync<GetTenantsDto>
    {
        protected readonly ILogger<GetTenants> _logger;
        protected GetTenantsParams _params;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        protected FlexAppContextBridge? _flexAppContext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetTenants(ILogger<GetTenants> logger, IFlexTenantRepository<FlexTenantBridge> repoTenantFactory)
        {
            _logger = logger;
            _repoTenantFactory = repoTenantFactory;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        public virtual GetTenants AssignParameters(GetTenantsParams @params)
        {
            _params = @params;
            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<GetTenantsDto>> Fetch()
        {
            IEnumerable<GetTenantsDto> result = null;

            result = await _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => !x.IsDeleted && x.Name != null && x.Name.StartsWith(_params.Name))
                    .Select(x => new GetTenantsDto
                    {
                        Id = x.Id,
                        Value = x.Name
                    }).ToListAsync();

            return result;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GetTenantsParams : DtoBridge
    {
        [Required(ErrorMessage = "TenantName is required")]
        public string Name { get; set; }
    }
}