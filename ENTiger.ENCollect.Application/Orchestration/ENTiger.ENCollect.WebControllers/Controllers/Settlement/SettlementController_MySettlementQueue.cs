using ENTiger.ENCollect.SettlementModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        
        [HttpGet()]
        [Authorize(Policy = "CanViewMyQueueSettlementPolicy")]
        [Route("queue")]
        [ProducesResponseType(typeof(CaseGroupSummaryDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MySettlementQueue([FromQuery] MySettlementQueueParams parameters)
        {
            return await RunQueryListServiceAsync<MySettlementQueueParams, CaseGroupSummaryDto>(
                        parameters, _processSettlementService.MySettlementQueue);
        }
    }
}