using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPut]
        [Route("communication/template/status/update")]
        [Authorize(Policy = "CanEnableCommunicationTemplatePolicy")]
        [Authorize(Policy = "CanDisableCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTemplateStatus([FromBody] UpdateTemplateStatusDto dto)
        {
            var result = RateLimit(dto, "enabledisable_communication_template");
            return result ?? await RunService(200, dto, _processCommunicationService.UpdateTemplateStatus);
        }
    }
}