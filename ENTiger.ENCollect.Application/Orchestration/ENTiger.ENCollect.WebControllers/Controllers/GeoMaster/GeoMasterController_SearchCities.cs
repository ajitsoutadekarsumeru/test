using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost()]
        [Route("Master/City")]
        [ProducesResponseType(typeof(IEnumerable<SearchCitiesDto>), 200)]
        public async Task<IActionResult> SearchCities([FromBody] SearchCitiesParams parameters)
        {
            return await RunQueryListServiceAsync<SearchCitiesParams, SearchCitiesDto>(parameters, _processGeoMasterService.SearchCities);
        }
    }
}