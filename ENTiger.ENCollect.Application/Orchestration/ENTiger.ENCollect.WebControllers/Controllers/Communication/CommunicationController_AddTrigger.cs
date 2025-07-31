using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost]
        [Route("communication/trigger/add")]
        [Authorize(Policy = "CanCreateCommunicationTriggerPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddTrigger([FromBody]AddTriggerDto dto)
        {
            var result = RateLimit(dto, "add_communication_trigger");
            return result ?? await RunService(201, dto, _processCommunicationService.AddTrigger);
        }
    }
}
