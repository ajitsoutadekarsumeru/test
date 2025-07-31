using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("payment/mycollection")]
        [Authorize(Policy = "CanSearchMyCollectionsPolicy")]
        [ProducesResponseType(typeof(MyCollectionsDto), 200)]
        public async Task<IActionResult> MyCollections([FromBody] MyCollectionsParams parameters)
        {
            return await RunQuerySingleServiceAsync<MyCollectionsParams, MyCollectionsDto>(parameters, _processCollectionsService.MyCollections);
        }
    }
}