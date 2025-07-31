using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/duplicate_send_sms")]
        [Authorize(Policy = "CanSendDuplicateReceiptPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SendPaymentCopyViaSMS([FromBody] SendPaymentCopyViaSMSDto dto)
        {
            var result = RateLimit(dto, "send_duplicate_payment_sms");
            return result ?? await RunService(201, dto, _processCollectionsService.SendPaymentCopyViaSMS);
        }
    }
}