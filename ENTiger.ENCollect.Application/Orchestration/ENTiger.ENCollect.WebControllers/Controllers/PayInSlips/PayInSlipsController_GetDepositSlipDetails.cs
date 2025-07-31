using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("depositslip/search/details")]
        [ProducesResponseType(typeof(GetDepositSlipDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepositSlipDetails([FromBody] GetDepositSlipDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetDepositSlipDetailsParams, GetDepositSlipDetailsDto>(parameters, _processPayInSlipsService.GetDepositSlipDetails);
        }
    }
}