using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        private readonly ProcessGeoMasterService _processGeoMasterService;

        public GeoMasterController(ProcessGeoMasterService processGeoMasterService, ILogger<GeoMasterController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processGeoMasterService = processGeoMasterService;
        }
    }
}