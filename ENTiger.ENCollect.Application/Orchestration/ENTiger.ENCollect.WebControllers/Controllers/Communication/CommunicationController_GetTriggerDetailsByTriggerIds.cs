using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost()]
        [Route("communication/trigger/ids")]
        //[Authorize(Policy = "CanViewCommunicationTriggerDetailsByTriggerIdsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTriggerDetailsByTriggerIdsDto>), 200)]
        public async Task<IActionResult> GetTriggerDetailsByTriggerIds([FromBody] GetTriggerDetailsByTriggerIdsParams parameters)
        {
            return RunQueryListService<GetTriggerDetailsByTriggerIdsParams, GetTriggerDetailsByTriggerIdsDto>(
                        parameters, _processCommunicationService.GetTriggerDetailsByTriggerIds);
        }
    }
}
