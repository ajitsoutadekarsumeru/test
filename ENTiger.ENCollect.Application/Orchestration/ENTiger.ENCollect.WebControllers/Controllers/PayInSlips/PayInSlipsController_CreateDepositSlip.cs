using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("Depositslip/create")]
        [Authorize(Policy = "CanCreateDepositSlipPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreateDepositSlip([FromBody] CreateDepositSlipDto dto)
        {
            var result = RateLimit(dto, "create_depositslip");
            return result ?? await RunService(201, dto, _processPayInSlipsService.CreateDepositSlip);
        }
    }
}