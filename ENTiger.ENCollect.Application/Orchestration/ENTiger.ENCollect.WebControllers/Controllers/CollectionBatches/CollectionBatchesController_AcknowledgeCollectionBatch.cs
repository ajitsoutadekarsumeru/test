using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpPost]
        [Route("branch/ackBatch")]
        [Authorize(Policy = "CanAcknowledgeBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AcknowledgeCollectionBatch([FromBody] AcknowledgeCollectionBatchDto dto)
        {
            var result = RateLimit(dto, "ack_Batch");
            return result ?? await RunService(200, dto, _processCollectionBatchesService.AcknowledgeCollectionBatch);
        }
    }
}