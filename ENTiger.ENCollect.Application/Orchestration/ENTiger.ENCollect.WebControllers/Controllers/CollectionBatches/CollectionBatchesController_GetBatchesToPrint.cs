using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class CollectionBatchesController : FlexControllerBridge<CollectionBatchesController>
    {
        [HttpGet()]
        [Route("batch/batchList")]
        [ProducesResponseType(typeof(IEnumerable<GetBatchesToPrintDto>), 200)]
        public async Task<IActionResult> GetBatchesToPrint([FromQuery] GetBatchesToPrintParams parameters)
        {
            return await RunQueryListServiceAsync<GetBatchesToPrintParams, GetBatchesToPrintDto>(parameters, _processCollectionBatchesService.GetBatchesToPrint);
        }
    }
}