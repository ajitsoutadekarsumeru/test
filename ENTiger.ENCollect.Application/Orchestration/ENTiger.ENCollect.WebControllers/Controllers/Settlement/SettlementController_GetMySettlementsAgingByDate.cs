using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("aging/date")]
        [ProducesResponseType(typeof(IEnumerable<GetMySettlementsAgingByDateDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMySettlementsAgingByDate([FromQuery]GetMySettlementsAgingByDateParams parameters)
        {
            return await RunQueryListServiceAsync<GetMySettlementsAgingByDateParams, GetMySettlementsAgingByDateDto>(
                        parameters, _processSettlementService.GetMySettlementsAgingByDate);
        }
    }
}
