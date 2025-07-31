using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{

    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("myreceipts/summary")]
        [Authorize(Policy = "CanViewMyReceiptsSummaryPolicy")]
        [ProducesResponseType(typeof(GetMyReceiptsSummaryDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyReceiptsSummary([FromQuery]GetMyReceiptsSummaryParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetMyReceiptsSummaryParams, GetMyReceiptsSummaryDto>(
                        parameters, _processCollectionsService.GetMyReceiptsSummary);
        }
    }
}
