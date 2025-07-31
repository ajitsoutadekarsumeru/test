using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("Collection/SendPaymentLink")]
        [Authorize(Policy = "CanSendPaymentLinkPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SendPaymentLink([FromBody] SendPaymentLinkDto dto)
        {
            var result = RateLimit(dto, "send_payment_link");
            return result ?? await RunService(201, dto, _processCollectionsService.SendPaymentLink);
        }
    }
}