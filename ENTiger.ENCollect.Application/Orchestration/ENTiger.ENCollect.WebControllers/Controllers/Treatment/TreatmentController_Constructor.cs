using ENCollect.ApiManagement.RateLimiter;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{

    [Authorize]
    [ServiceFilter(typeof(CheckUserSessionAccess))]
    [Route("api/mvp")]
    [ApiController]
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        private readonly ProcessTreatmentService _processTreatmentService;
        private readonly IFileTransferUtility _fileTransferUtility;

        public TreatmentController(ProcessTreatmentService processTreatmentService, ILogger<TreatmentController> logger, IRateLimiter rateLimiter,IFlexHostHttpContextAccesorBridge appHttpContextAccessor, IFileTransferUtility fileTransferUtility) : base(logger, rateLimiter)
        {
            _processTreatmentService = processTreatmentService;
            _fileTransferUtility = fileTransferUtility;
        }
    }
}