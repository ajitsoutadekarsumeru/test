using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.HierarchyModule
{
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class HierarchyController : FlexControllerBridge<HierarchyController>
    {
        readonly ProcessHierarchyService _processHierarchyService;

        public HierarchyController(ProcessHierarchyService processHierarchyService, ILogger<HierarchyController> logger, IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IRateLimiter rateLimiter) : base(logger, rateLimiter)
        {
            _processHierarchyService = processHierarchyService;
        }
    }
}
