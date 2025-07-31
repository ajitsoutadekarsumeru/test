using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpPost]
        [Route("batch/paymentreceipt")]
        [Authorize(Policy = "CanUpdateBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateCollectionBatch([FromBody] UpdateCollectionBatchDto dto)
        {
            var result = RateLimit(dto, "update_batch");
            return result ?? await RunService(200, dto, _processCollectionBatchesService.UpdateCollectionBatch);
        }
    }
}