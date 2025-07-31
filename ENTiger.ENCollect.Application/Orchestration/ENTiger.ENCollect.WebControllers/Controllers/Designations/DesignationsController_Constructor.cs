using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        private readonly ProcessDesignationsService _processDesignationsService;

        public DesignationsController(ProcessDesignationsService processDesignationsService, ILogger<DesignationsController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processDesignationsService = processDesignationsService;
        }
    }
}