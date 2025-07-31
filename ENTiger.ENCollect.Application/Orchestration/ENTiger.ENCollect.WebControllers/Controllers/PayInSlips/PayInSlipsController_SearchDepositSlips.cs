using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("receipt/depositslip/search")]
        [Authorize(Policy = "CanSearchMyDepositSlipsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchDepositSlipsDto>), 200)]
        public async Task<IActionResult> SearchDepositSlips([FromBody] SearchDepositSlipsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchDepositSlipsParams, SearchDepositSlipsDto>(parameters, _processPayInSlipsService.SearchDepositSlips);
        }
    }
}