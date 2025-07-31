using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        private readonly ProcessSegmentationService _processSegmentationService;

        public SegmentationController(ProcessSegmentationService processSegmentationService, ILogger<SegmentationController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processSegmentationService = processSegmentationService;
        }
    }
}