using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api")]
    [ApiController]
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        private readonly ProcessCommonService _processCommonService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public CommonController(ProcessCommonService processCommonService, ILogger<CommonController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processCommonService = processCommonService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}