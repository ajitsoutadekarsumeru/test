using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp/[controller]/")]
    [ApiController]
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        readonly ProcessSettlementService _processSettlementService;

        public SettlementController(ProcessSettlementService processSettlementService, 
            ILogger<SettlementController> logger,
            IRateLimiter rateLimiter,
            IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger,rateLimiter)
        {
            _processSettlementService = processSettlementService;
        }
    }
}
