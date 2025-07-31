using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        private readonly ProcessPayInSlipsService _processPayInSlipsService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public PayInSlipsController(ProcessPayInSlipsService processPayInSlipsService, ILogger<PayInSlipsController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processPayInSlipsService = processPayInSlipsService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}