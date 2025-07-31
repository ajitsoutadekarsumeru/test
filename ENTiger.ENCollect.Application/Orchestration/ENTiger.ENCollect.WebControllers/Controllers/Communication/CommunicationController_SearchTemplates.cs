using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class CommunicationController : FlexControllerBridge<CommunicationController>
    {
        [HttpPost()]
        [Route("communication/template/search")]
        [Authorize(Policy = "CanSearchCommunicationTemplatePolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchTemplatesDto>), 200)]
        public async Task<IActionResult> SearchTemplates([FromBody] SearchTemplatesParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchTemplatesParams, SearchTemplatesDto>(
                        parameters, _processCommunicationService.SearchTemplates);
        }
    }
}