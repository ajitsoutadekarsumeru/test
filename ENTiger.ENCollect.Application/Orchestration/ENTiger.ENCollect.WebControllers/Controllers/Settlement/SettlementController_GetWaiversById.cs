using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("waivers")]
        [ProducesResponseType(typeof(GetWaiversByIdDto), 200)]
        public async Task<IActionResult> GetWaiversById([FromQuery]GetWaiversByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetWaiversByIdParams, GetWaiversByIdDto>(
                            parameters, _processSettlementService.GetWaiversById);
        }
    }
}
