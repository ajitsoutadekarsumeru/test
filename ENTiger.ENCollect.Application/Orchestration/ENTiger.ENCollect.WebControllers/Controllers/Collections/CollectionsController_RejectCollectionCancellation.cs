using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/ereceipt/requested/cancellation/rejected")]
        [Authorize(Policy = "CanRejectReceiptCancellationRequestPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> RejectCollectionCancellation([FromBody] RejectCollectionCancellationDto dto)
        {
            var result = RateLimit(dto, "reject_receipt_cancellation");
            return result ?? await RunService(201, dto, _processCollectionsService.RejectCollectionCancellation);
        }
    }
}