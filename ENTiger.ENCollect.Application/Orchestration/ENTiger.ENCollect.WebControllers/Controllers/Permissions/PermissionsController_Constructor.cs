using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class PermissionsController : FlexControllerBridge<PermissionsController>
    {
        readonly ProcessPermissionsService _processPermissionsService;

        public PermissionsController(ProcessPermissionsService processPermissionsService, ILogger<PermissionsController> logger, IRateLimiter rateLimiter, IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processPermissionsService = processPermissionsService;
        }
    }
}
