using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/batch/details")]
        [ProducesResponseType(typeof(IEnumerable<GetCollectionBatchesDto>), 200)]
        public async Task<IActionResult> GetCollectionBatches([FromBody] GetCollectionBatchesParams parameters)
        {
            return await RunQueryListServiceAsync<GetCollectionBatchesParams, GetCollectionBatchesDto>(parameters, _processPayInSlipsService.GetCollectionBatches);
        }
    }
}