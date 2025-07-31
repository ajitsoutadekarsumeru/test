using ENCollect.ApiManagement.RateLimiter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        private readonly ProcessCollectionBatchesService _processCollectionBatchesService;

        public CollectionBatchesController(ProcessCollectionBatchesService processCollectionBatchesService, ILogger<CollectionBatchesController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor) : base(logger, rateLimiter)
        {
            _processCollectionBatchesService = processCollectionBatchesService;
        }
    }
}