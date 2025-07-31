using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class UserSearchCriteriaController : FlexControllerBridge<UserSearchCriteriaController>
    {
        private readonly ProcessUserSearchCriteriaService _processUserSearchCriteriaService;

        public UserSearchCriteriaController(ProcessUserSearchCriteriaService processUserSearchCriteriaService, ILogger<UserSearchCriteriaController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processUserSearchCriteriaService = processUserSearchCriteriaService;
        }
    }
}