using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/CityMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchCityDto>), 200)]
        public async Task<IActionResult> SearchCity(string search)
        {
            SearchCityParams parameters = new SearchCityParams();
            parameters.search = search;

            return await RunQueryListServiceAsync<SearchCityParams, SearchCityDto>(
                        parameters, _processGeographyService.SearchCity);
        }
    }
}