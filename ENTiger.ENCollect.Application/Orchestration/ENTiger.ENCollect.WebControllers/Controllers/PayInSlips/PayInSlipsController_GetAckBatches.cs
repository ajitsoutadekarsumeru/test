using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpPost]
        [Route("payinslip/batchIds")]
        [ProducesResponseType(typeof(IEnumerable<GetAckBatchesDto>), 200)]
        public async Task<IActionResult> GetAckBatches([FromBody] GetAckBatchesParams parameters)
        {
            return await RunQueryListServiceAsync<GetAckBatchesParams, GetAckBatchesDto>(parameters, _processPayInSlipsService.GetAckBatches);
        }
    }
}