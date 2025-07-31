using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpGet]
        [Route("batch/search/{batchId}")]
        [ProducesResponseType(typeof(GetBatchByIdDto), 200)]
        public async Task<IActionResult> GetBatchById(string batchId)
        {
            GetBatchByIdParams parameters = new GetBatchByIdParams();
            parameters.BatchId = batchId;
            return await RunQuerySingleServiceAsync<GetBatchByIdParams, GetBatchByIdDto>(parameters, _processCollectionBatchesService.GetBatchById);
        }
    }
}