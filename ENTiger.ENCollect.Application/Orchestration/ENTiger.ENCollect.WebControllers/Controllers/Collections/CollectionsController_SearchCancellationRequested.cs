using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("payment/ereceipt/requested/cancellation/search")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchCancellationRequestedDto>), 200)]
        public async Task<IActionResult> SearchCancellationRequested([FromBody] SearchCancellationRequestedParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchCancellationRequestedParams, SearchCancellationRequestedDto>(parameters, _processCollectionsService.SearchCancellationRequested);
        }
    }
}