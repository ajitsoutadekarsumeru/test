using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("summary")]
        [ProducesResponseType(typeof(MySettlementsSummaryDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MySettlements([FromQuery] MySettlementsSummaryParams parameters)
        {
            return await RunQuerySingleServiceAsync<MySettlementsSummaryParams, MySettlementsSummaryDto>(
                        parameters, _processSettlementService.MySettlements);
        }
    }
}