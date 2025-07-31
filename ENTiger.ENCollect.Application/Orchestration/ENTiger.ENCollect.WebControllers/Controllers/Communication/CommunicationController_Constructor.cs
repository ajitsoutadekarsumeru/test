using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        private readonly ProcessCommunicationService _processCommunicationService;

        public CommunicationController(ProcessCommunicationService processCommunicationService, ILogger<CommunicationController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processCommunicationService = processCommunicationService;
        }
    }
}