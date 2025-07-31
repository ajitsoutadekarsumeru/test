using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("get/collectors")]
        [ProducesResponseType(typeof(IEnumerable<GetCollectorsDto>), 200)]
        public async Task<IActionResult> GetCollectors()
        {
            GetCollectorsParams parameters = new GetCollectorsParams();
            return await RunQueryListServiceAsync<GetCollectorsParams, GetCollectorsDto>(parameters, _processCollectionsService.GetCollectors);
        }
    }
}