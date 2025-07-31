using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        private readonly ProcessFeedbackService _processFeedbackService;
        private readonly IFileTransferUtility _fileTransferUtility;
        public FeedbackController(ProcessFeedbackService processFeedbackService, ILogger<FeedbackController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processFeedbackService = processFeedbackService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}