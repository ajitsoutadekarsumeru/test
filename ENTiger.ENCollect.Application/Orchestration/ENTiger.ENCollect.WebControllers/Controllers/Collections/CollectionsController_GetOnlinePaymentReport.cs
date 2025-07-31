using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("collection/online/report")]
        [ProducesResponseType(typeof(IEnumerable<GetOnlinePaymentReportDto>), 200)]
        public async Task<IActionResult> GetOnlinePaymentReport([FromBody] GetOnlinePaymentReportParams parameters)
        {
            return await RunQueryListServiceAsync<GetOnlinePaymentReportParams, GetOnlinePaymentReportDto>(
                        parameters, _processCollectionsService.GetOnlinePaymentReport);
        }
    }
}