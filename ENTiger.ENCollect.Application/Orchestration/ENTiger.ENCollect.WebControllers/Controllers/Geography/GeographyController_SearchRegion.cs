using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/RegionsMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchRegionDto>), 200)]
        public async Task<IActionResult> SearchRegion(string search)
        {
            SearchRegionParams parameters = new SearchRegionParams();
            parameters.search = search;
            return await RunQueryListServiceAsync<SearchRegionParams, SearchRegionDto>(
                        parameters, _processGeographyService.SearchRegion);
        }
    }
}