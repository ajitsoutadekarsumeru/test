using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPut]
        [Route("communication/trigger/update")]
        [Authorize(Policy = "CanUpdateCommunicationTriggerPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTrigger([FromBody]UpdateTriggerDto dto)
        {
            var result = RateLimit(dto, "update_communication_trigger");
            return result ?? await RunService(200, dto, _processCommunicationService.UpdateTrigger);
        }
    }
}
