using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("payment/searchByBank")]
        [ProducesResponseType(typeof(IEnumerable<GetCollectionsDto>), 200)]
        public async Task<IActionResult> GetCollections([FromBody] GetCollectionsParams parameters)
        {
            return await RunQueryListServiceAsync<GetCollectionsParams, GetCollectionsDto>(parameters, _processCollectionsService.GetCollections);
        }
    }
}