using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("viewDepositSlipDetails")]
        [ProducesResponseType(typeof(GetDepositSlipByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepositSlipById([FromBody] GetDepositSlipByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetDepositSlipByIdParams, GetDepositSlipByIdDto>(parameters, _processPayInSlipsService.GetDepositSlipById);
        }
    }
}