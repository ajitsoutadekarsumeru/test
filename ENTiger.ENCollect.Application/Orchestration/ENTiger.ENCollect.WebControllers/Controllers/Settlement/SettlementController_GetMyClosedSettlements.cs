using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("closed/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetMyClosedSettlementsDto>), 200)]
        public async Task<IActionResult> GetMyClosedSettlements([FromQuery]GetMyClosedSettlementsParams parameters)
        {
            return await RunQueryListServiceAsync<GetMyClosedSettlementsParams, GetMyClosedSettlementsDto>(
                        parameters, _processSettlementService.GetMyClosedSettlements);
        }
    }
}
