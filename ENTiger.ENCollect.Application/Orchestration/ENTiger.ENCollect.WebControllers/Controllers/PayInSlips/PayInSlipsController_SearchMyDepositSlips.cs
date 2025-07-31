using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("MyDepositeSlip/Search")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchMyDepositSlipsDto>), 200)]
        public async Task<IActionResult> SearchMyDepositSlips([FromBody] SearchMyDepositSlipsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchMyDepositSlipsParams, SearchMyDepositSlipsDto>(parameters, _processPayInSlipsService.SearchMyDepositSlips);
        }
    }
}