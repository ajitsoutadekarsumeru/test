using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/print")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdatePrintStatus([FromBody] UpdatePrintStatusDto dto)
        {
            return await RunService(200, dto, _processPayInSlipsService.UpdatePrintStatus);
        }
    }
}