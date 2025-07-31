using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpGet()]
        [Route("payinslip/view/{id}")]
        [Authorize(Policy = "CanViewPISPolicy")]
        [ProducesResponseType(typeof(GetPayInSlipByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPayInSlipById(string Id)
        {
            GetPayInSlipByIdParams parameters = new GetPayInSlipByIdParams();
            parameters.Id = Id;

            return await RunQuerySingleServiceAsync<GetPayInSlipByIdParams, GetPayInSlipByIdDto>(parameters, _processPayInSlipsService.GetPayInSlipById);
        }
    }
}