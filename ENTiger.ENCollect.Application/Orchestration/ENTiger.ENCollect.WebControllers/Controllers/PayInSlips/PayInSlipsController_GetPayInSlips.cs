using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpGet()]
        [Route("payinslip/payinslips")]
        [ProducesResponseType(typeof(IEnumerable<GetPayInSlipsDto>), 200)]
        public async Task<IActionResult> GetPayInSlips()
        {
            GetPayInSlipsParams parameters = new GetPayInSlipsParams();
            return await RunQueryListServiceAsync<GetPayInSlipsParams, GetPayInSlipsDto>(parameters, _processPayInSlipsService.GetPayInSlips);
        }
    }
}