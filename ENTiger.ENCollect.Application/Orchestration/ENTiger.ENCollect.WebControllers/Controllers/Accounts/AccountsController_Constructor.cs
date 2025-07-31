using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        private readonly ProcessAccountsService _processAccountsService;

        public AccountsController(ProcessAccountsService processAccountsService, ILogger<AccountsController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processAccountsService = processAccountsService;
        }
    }
}