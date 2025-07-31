using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DepartmentsModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class DepartmentsController : FlexControllerBridge<DepartmentsController>
    {
        private readonly ProcessDepartmentsService _processDepartmentsService;

        public DepartmentsController(ProcessDepartmentsService processDepartmentsService, ILogger<DepartmentsController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processDepartmentsService = processDepartmentsService;
        }
    }
}