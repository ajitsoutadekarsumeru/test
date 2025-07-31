using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class BaseBranchesController : FlexControllerBridge<BaseBranchesController>
    {
        private readonly ProcessBaseBranchesService _processBaseBranchesService;

        public BaseBranchesController(ProcessBaseBranchesService processBaseBranchesService, ILogger<BaseBranchesController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processBaseBranchesService = processBaseBranchesService;
        }
    }
}