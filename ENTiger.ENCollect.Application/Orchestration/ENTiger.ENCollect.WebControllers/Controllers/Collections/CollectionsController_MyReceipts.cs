using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("payment/myreceipts")]
        [Authorize(Policy = "CanSearchMyReceiptsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<MyReceiptsDto>), 200)]
        public async Task<IActionResult> MyReceipts([FromBody] MyReceiptsParams parameters)
        {
            return await RunQueryPagedServiceAsync<MyReceiptsParams, MyReceiptsDto>(parameters, _processCollectionsService.MyReceipts);
        }
    }
}