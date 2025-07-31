using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost]
        [Route("communication/template/add")]
        [Authorize(Policy = "CanCreateCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddTemplate([FromBody] AddTemplateDto dto)
        {
            var result = RateLimit(dto, "add_communication_template");
            return result ?? await RunService(201, dto, _processCommunicationService.AddTemplate);
        }
    }
}