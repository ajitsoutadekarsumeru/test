using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("search/collection")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchCollectionsDto>), 200)]
        public async Task<IActionResult> SearchCollections([FromBody] SearchCollectionsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchCollectionsParams, SearchCollectionsDto>(parameters, _processCollectionsService.SearchCollections);
        }
    }
}