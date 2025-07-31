using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/search")]
        [Authorize(Policy = "CanSearchPISPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetPayInSlipsForAckDto>), 200)]
        public async Task<IActionResult> GetPayInSlipsForAck([FromBody] GetPayInSlipsForAckParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetPayInSlipsForAckParams, GetPayInSlipsForAckDto>(parameters, _processPayInSlipsService.GetPayInSlipsForAck);
        }
    }
}