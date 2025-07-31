using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("receipts/myreceiptallocations")]
        [ProducesResponseType(typeof(IEnumerable<GetMyReceiptAllocationsDto>), 200)]
        public async Task<IActionResult> GetMyReceiptAllocations()
        {
            GetMyReceiptAllocationsParams parameters = new GetMyReceiptAllocationsParams();
            return await RunQueryListServiceAsync<GetMyReceiptAllocationsParams, GetMyReceiptAllocationsDto>(parameters, _processCollectionsService.GetMyReceiptAllocations);
        }
    }
}