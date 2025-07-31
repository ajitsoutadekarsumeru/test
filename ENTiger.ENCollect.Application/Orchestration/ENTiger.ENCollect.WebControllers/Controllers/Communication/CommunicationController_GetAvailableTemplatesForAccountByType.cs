using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpGet()]
        [Route("communication/account/templates")]
        [Authorize(Policy = "CanGetCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetAvailableTemplatesForAccountByTypeDto>), 200)]
        public async Task<IActionResult> GetAvailableTemplatesForAccountByType([FromQuery]GetAvailableTemplatesForAccountByTypeParams parameters)
        {
            return RunQueryListService<GetAvailableTemplatesForAccountByTypeParams, GetAvailableTemplatesForAccountByTypeDto>(
                        parameters, _processCommunicationService.GetAvailableTemplatesForAccountByType);
        }
    }
}
