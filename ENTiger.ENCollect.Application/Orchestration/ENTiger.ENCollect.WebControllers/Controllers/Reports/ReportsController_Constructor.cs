using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ReportsModule
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public partial class ReportsController : FlexControllerBridge<ReportsController>
    {
        readonly ProcessReportsService _processReportsService;

        public ReportsController(ProcessReportsService processReportsService, 
            ILogger<ReportsController> logger, 
            IFlexHostHttpContextAccesorBridge appHttpContextAccessor,
            IRateLimiter rateLimiter) : base(logger, rateLimiter)
        {
            _processReportsService = processReportsService;
        }
    }
}
