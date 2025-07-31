using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    [Route("api")]
    [ApiController]
    public partial class PublicController : FlexControllerBridge<PublicController>
    {
        private readonly ProcessPublicService _processPublicService;

        public PublicController(ProcessPublicService processPublicService, ILogger<PublicController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processPublicService = processPublicService;
        }
    }
}