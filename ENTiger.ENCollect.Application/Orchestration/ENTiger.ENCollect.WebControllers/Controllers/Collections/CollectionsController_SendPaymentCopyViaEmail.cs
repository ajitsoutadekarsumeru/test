using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/duplicate_send_email")]
        [Authorize(Policy = "CanSendDuplicateReceiptPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SendPaymentCopyViaEmail([FromBody] SendPaymentCopyViaEmailDto dto)
        {
            var result = RateLimit(dto, "send_duplicate_payment_email");
            return result ?? await RunService(201, dto, _processCollectionsService.SendPaymentCopyViaEmail);
        }
    }
}