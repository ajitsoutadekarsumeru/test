using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpPost()]
        [Route("batch/details")]
        [ProducesResponseType(typeof(FlexiPagedList<GetBatchCollectionsDto>), 200)]
        public async Task<IActionResult> GetBatchCollections([FromBody] GetBatchCollectionsParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetBatchCollectionsParams, GetBatchCollectionsDto>(parameters, _processCollectionBatchesService.GetBatchCollections);
        }
    }
}