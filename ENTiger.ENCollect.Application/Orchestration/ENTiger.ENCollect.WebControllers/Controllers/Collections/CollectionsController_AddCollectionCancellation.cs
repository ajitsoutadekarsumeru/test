using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPut]
        [Route("payment/ereceipt/cancellation/request")]
        [Authorize(Policy = "CanCreateReceiptCancellationRequestPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddCollectionCancellation([FromBody] AddCollectionCancellationDto dto)
        {
            var result = RateLimit(dto, "request_receipt_cancellation");
            return result ?? await RunService(201, dto, _processCollectionsService.AddCollectionCancellation);
        }
    }
}