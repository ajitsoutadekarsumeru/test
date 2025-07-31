using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost()]
        [Route("communication/trigger/search")]
        [Authorize(Policy = "CanSearchCommunicationTriggerPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchTriggersDto>), 200)] 
        public async Task<IActionResult> SearchTriggers([FromBody] SearchTriggersParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchTriggersParams, SearchTriggersDto>(
                      parameters, _processCommunicationService.SearchTriggers);
        }
    }
}
