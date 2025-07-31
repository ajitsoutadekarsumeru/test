using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpPost()]
        [Route("batch/search")]
        [Authorize(Policy = "CanSearchBatchPolicy")]
        [ProducesResponseType(typeof(GetCollectionBatchDto), 200)]
        public async Task<IActionResult> GetCollectionBatches([FromBody] GetCollectionBatchParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetCollectionBatchParams, GetCollectionBatchDto>(parameters, _processCollectionBatchesService.GetCollectionBatches);
        }
    }
}