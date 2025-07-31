using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("open/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetMyOpenSettlementsDto>), 200)]
        public async Task<IActionResult> GetMyOpenSettlements([FromQuery]GetMyOpenSettlementsParams parameters)
        {
            return await RunQueryListServiceAsync<GetMyOpenSettlementsParams, GetMyOpenSettlementsDto>(
                        parameters, _processSettlementService.GetMyOpenSettlements);
        }
    }
}
