using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        private readonly ProcessAllocationService _processAllocationService;
        private readonly IFileTransferUtility _fileTransferUtility;
        public AllocationController(ProcessAllocationService processAllocationService, ILogger<AllocationController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processAllocationService = processAllocationService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}