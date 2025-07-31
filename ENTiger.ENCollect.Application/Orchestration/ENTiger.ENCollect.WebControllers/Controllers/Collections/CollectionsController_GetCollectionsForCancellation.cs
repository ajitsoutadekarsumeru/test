using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/ereceipt/cancellation/search")]
        [ProducesResponseType(typeof(FlexiPagedList<GetCollectionsForCancellationDto>), 200)]
        public async Task<IActionResult> GetCollectionsForCancellation([FromBody] GetCollectionsForCancellationParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetCollectionsForCancellationParams, GetCollectionsForCancellationDto>(parameters, _processCollectionsService.GetCollectionsForCancellation);
        }
    }
}