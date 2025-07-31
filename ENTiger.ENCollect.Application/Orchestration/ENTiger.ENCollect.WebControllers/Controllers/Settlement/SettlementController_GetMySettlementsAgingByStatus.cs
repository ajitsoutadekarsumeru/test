using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("aging/status")]
        [ProducesResponseType(typeof(IEnumerable<GetMySettlementsAgingByStatusDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMySettlementsAgingByStatus([FromQuery]GetMySettlementsAgingByStatusParams parameters)
        {
            return await RunQueryListServiceAsync<GetMySettlementsAgingByStatusParams, GetMySettlementsAgingByStatusDto>(
                        parameters, _processSettlementService.GetMySettlementsAgingByStatus);
        }
    }
}
