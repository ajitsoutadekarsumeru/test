using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        private readonly ProcessCollectionsService _processCollectionsService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public CollectionsController(ProcessCollectionsService processCollectionsService, ILogger<CollectionsController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processCollectionsService = processCollectionsService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}