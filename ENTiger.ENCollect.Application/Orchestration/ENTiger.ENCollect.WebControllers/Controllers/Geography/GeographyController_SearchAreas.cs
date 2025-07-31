using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/AreaMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchAreaDto>), 200)]
        public async Task<IActionResult> SearchAreas(string search)
        {
            SearchAreasParams parameters = new SearchAreasParams() { query = search };
            return await RunQueryListServiceAsync<SearchAreasParams, SearchAreaDto>(parameters, _processGeographyService.SearchAreas);
        }
    }
}