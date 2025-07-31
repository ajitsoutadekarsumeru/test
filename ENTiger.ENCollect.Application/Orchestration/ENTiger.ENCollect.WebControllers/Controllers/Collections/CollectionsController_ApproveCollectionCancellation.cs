using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/ereceipt/requested/cancellation/approved")]
        [Authorize(Policy = "CanApproveReceiptCancellationRequestPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ApproveCollectionCancellation([FromBody] ApproveCollectionCancellationDto dto)
        {
            var result = RateLimit(dto, "approve_receipt_cancellation");
            return result ?? await RunService(201, dto, _processCollectionsService.ApproveCollectionCancellation);
        }
    }
}