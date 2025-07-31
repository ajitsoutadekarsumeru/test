using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost()]
        [Route("communication/templates/ids")]
        //[Authorize(Policy = "CanViewCommunicationTemplateDetailsByTemplateIdsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTemplateDetailsByTemplateIdsDto>), 200)]
        public async Task<IActionResult> GetTemplateDetailsByTemplateIds([FromBody] GetTemplateDetailsByTemplateIdsParams parameters)
        {
            return RunQueryListService<GetTemplateDetailsByTemplateIdsParams, GetTemplateDetailsByTemplateIdsDto>(
                        parameters, _processCommunicationService.GetTemplateDetailsByTemplateIds);
        }
    }
}
