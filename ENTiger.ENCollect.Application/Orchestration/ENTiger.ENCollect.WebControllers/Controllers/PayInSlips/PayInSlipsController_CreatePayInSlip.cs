using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/create")]
        [Authorize(Policy = "CanCreatePISPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreatePayInSlip([FromBody] CreatePayInSlipDto dto)
        {
            var result = RateLimit(dto, "create_payinslip");
            return result ?? await RunService(201, dto, _processPayInSlipsService.CreatePayInSlip);
        }
    }
}