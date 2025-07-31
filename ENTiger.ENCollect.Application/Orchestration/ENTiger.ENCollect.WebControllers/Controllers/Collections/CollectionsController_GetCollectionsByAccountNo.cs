using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("receipts/account/last")]
        [Authorize(Policy = "CanViewLastThreePaymentsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetCollectionsByAccountNoDto>), 200)]
        public async Task<IActionResult> GetCollectionsByAccountNo([FromBody] GetCollectionsByAccountNoParams parameters)
        {
            return await RunQueryListServiceAsync<GetCollectionsByAccountNoParams, GetCollectionsByAccountNoDto>(parameters, _processCollectionsService.GetCollectionsByAccountNo);
        }
    }
}