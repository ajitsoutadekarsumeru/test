using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdatePayInSlip([FromBody] UpdatePayInSlipDto dto)
        {
            var result = RateLimit(dto, "update_payinslip");
            return result ?? await RunService(200, dto, _processPayInSlipsService.UpdatePayInSlip);
        }
    }
}