using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/mvp")]
    [ApiController]
    public partial class DevicesController : FlexControllerBridge<DevicesController>
    {
        private readonly ProcessDevicesService _processDevicesService;

        public DevicesController(ProcessDevicesService processDevicesService, ILogger<DevicesController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processDevicesService = processDevicesService;
        }
    }
}