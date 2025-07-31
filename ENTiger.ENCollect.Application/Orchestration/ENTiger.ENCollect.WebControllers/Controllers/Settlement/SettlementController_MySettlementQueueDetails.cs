using ENTiger.ENCollect.SettlementModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        
        [HttpPost()]
        [Authorize(Policy = "CanViewMyQueueSettlementPolicy")]
        [Route("queue/details")]
        [ProducesResponseType(typeof(MySettlementQueueDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MySettlementQueueDetails([FromBody] MySettlementQueueDetailsParams parameters)
        {
            return await RunQueryPagedServiceAsync<MySettlementQueueDetailsParams, MySettlementQueueDetailsDto>(
                        parameters, _processSettlementService.MySettlementQueueDetails);
        }
    }
}