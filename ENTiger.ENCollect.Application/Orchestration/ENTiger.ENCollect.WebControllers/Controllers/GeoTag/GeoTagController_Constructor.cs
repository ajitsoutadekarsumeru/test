using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        private readonly ProcessGeoTagService _processGeoTagService;

        public GeoTagController(ProcessGeoTagService processGeoTagService, ILogger<GeoTagController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processGeoTagService = processGeoTagService;
        }
    }
}