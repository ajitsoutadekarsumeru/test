using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPut]
        [Route("communication/trigger/status/update")]
        [Authorize(Policy = "CanEnableCommunicationTriggerPolicy")]
        [Authorize(Policy = "CanDisableCommunicationTriggerPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTriggerStatus([FromBody]UpdateTriggerStatusDto dto)
        {
            var result = RateLimit(dto, "update_communication_trigger_Status");
            return result ?? await RunService(200, dto, _processCommunicationService.UpdateTriggerStatus);
        }
    }
}
