using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/ack")]
        [Authorize(Policy = "CanAcknowledgePISPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AckPayInSlip([FromBody] AckPayInSlipDto dto)
        {
            var result = RateLimit(dto, "ack_payinslip");
            return result ?? await RunService(200, dto, _processPayInSlipsService.AckPayInSlip);
        }
    }
}