using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpPost]
        [Route("add/collectionBatch")]
        [Authorize(Policy = "CanCreateBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddCollectionBatch([FromBody] AddCollectionBatchDto dto)
        {
            var result = RateLimit(dto, "add_Batch");
            return result ?? await RunService(201, dto, _processCollectionBatchesService.AddCollectionBatch);
        }
    }
}