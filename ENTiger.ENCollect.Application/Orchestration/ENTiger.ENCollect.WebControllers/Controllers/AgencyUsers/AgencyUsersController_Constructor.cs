using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        private readonly ProcessAgencyUsersService _processAgencyUsersService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public AgencyUsersController(ProcessAgencyUsersService processAgencyUsersService, ILogger<AgencyUsersController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processAgencyUsersService = processAgencyUsersService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}