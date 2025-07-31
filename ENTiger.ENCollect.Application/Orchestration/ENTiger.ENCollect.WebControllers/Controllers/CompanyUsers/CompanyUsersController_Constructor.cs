using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        private readonly ProcessCompanyUsersService _processCompanyUsersService;

        public CompanyUsersController(ProcessCompanyUsersService processCompanyUsersService, ILogger<CompanyUsersController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processCompanyUsersService = processCompanyUsersService;
        }
    }
}