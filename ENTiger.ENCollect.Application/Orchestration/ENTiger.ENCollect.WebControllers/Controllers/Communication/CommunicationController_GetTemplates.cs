using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost()]
        [Route("communication/templates")]
        [Authorize(Policy = "CanGetCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTemplatesDto>), 200)]
        public async Task<IActionResult> GetTemplates([FromBody]GetTemplatesParams parameters)
        {
            return await RunQueryListServiceAsync<GetTemplatesParams, GetTemplatesDto>(
                        parameters, _processCommunicationService.GetTemplates);
        }
    }
}
