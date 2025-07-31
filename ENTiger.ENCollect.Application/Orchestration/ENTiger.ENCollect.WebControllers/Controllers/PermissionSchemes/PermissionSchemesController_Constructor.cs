using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        readonly ProcessPermissionSchemesService _processPermissionSchemesService;

        public PermissionSchemesController(ProcessPermissionSchemesService processPermissionSchemesService, ILogger<PermissionSchemesController> logger, IRateLimiter rateLimiter, IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processPermissionSchemesService = processPermissionSchemesService;
        }
    }
}
