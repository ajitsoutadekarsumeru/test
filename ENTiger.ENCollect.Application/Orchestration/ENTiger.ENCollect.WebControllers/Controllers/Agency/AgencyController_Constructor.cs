using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        private readonly ProcessAgencyService _processAgencyService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public AgencyController(ProcessAgencyService processAgencyService, ILogger<AgencyController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processAgencyService = processAgencyService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}