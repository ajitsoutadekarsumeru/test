using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    [Route("api")]
    [ApiController]
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        private readonly ProcessApplicationUsersService _processApplicationUsersService;
        private readonly GoogleSettings _googleSettings;

        public ApplicationUsersController(ProcessApplicationUsersService processApplicationUsersService, ILogger<ApplicationUsersController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IOptions<GoogleSettings> googleSettings) : base(logger, rateLimiter)
        {
            _processApplicationUsersService = processApplicationUsersService;
            _googleSettings = googleSettings.Value;
        }
    }
}