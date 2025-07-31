using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("search/acked/collection")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchAckedCollectionsDto>), 200)]
        public async Task<IActionResult> SearchAckedCollections([FromBody] SearchAckedCollectionsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchAckedCollectionsParams, SearchAckedCollectionsDto>(parameters, _processCollectionsService.SearchAckedCollections);
        }
    }
}