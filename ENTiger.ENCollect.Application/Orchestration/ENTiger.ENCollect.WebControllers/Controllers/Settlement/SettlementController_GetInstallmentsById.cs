using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("installments")]
        [ProducesResponseType(typeof(GetInstallmentsByIdDto), 200)]
        public async Task<IActionResult> GetInstallmentsById([FromQuery]GetInstallmentsByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetInstallmentsByIdParams, GetInstallmentsByIdDto>(
                    parameters, _processSettlementService.GetInstallmentsById);
        }
    }    
}
