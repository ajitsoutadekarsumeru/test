using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPut]
        [Route("communication/template/update")]
        [Authorize(Policy = "CanUpdateCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTemplate([FromBody] UpdateTemplateDto dto)
        {
            var result = RateLimit(dto, "update_communication_template");
            return result ?? await RunService(200, dto, _processCommunicationService.UpdateTemplate);
        }
    }
}